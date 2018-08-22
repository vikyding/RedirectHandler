// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Net;


namespace Msgraph
{
    /**
     * 
     * */
    public class RedirectHandler : DelegatingHandler
    {

        private const int maxRedirects = 5;


        /// <summary>
        /// Constructs a new <see cref="RedirectHandler"/> 
        /// </summary>
        /// <param name="innerHandler">A http message handler to pass to the <see cref="InnerHandler"/> for sending requests.</param>
        public RedirectHandler(HttpMessageHandler innerHandler)
        {
            InnerHandler = innerHandler;
        }

        /// <summary>
        /// Sends the Request 
        /// </summary>
        /// <param name="request">The http request message <see cref="HttpRequestMessage"/></param>
        /// <param name="cancellationToken">The cancellationToken <see cref="CancellationToken"/></param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // check request whether it's permanetly redirected
            // TO-DO

            // send request first time to get response
            var response = await base.SendAsync(request, cancellationToken);

            // check response status code 
            if (IsRedirect(response.StatusCode))
            {

                // general copy request with internal copyRequest(see copyRequest for details) Method 
                var newRequest = CopyRequest(request, response);
                StreamContent content = null;

                if (request.Content != null && request.Content.Headers.ContentLength != 0)
                {
                    content = new StreamContent(request.Content.ReadAsStreamAsync().Result);
                }

                var redirectCount = 0;

                // check whether redirect count over maxRedirects
                while (redirectCount++ < maxRedirects)
                {
                    // status code == 308 : add permanent redirection to cache 
                    // TO-DO

                    // status code == 303: change request method from post to get and content to be null
                    if (response.StatusCode == HttpStatusCode.SeeOther)
                    {
                        newRequest.Content = null;
                        newRequest.Method = HttpMethod.Get;
                    }
                    else
                    {
                        newRequest.Content = content;
                        newRequest.Method = request.Method;
                    }

                    // Set newRequestUri from response
                    newRequest.RequestUri = response.Headers.Location;

                    // Remove Auth if unneccessary
                    if (String.Compare(newRequest.RequestUri.Host, request.RequestUri.Host, StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        newRequest.Headers.Authorization = null;
                        
                    }

                    // Send redirect request to get reponse      
                    response = await base.SendAsync(newRequest, cancellationToken);

                    // Check response status code
                    if (!IsRedirect(response.StatusCode))
                    {
                        return response;
                    }
                }
                
            }
            return response;

        }

        /// <summary>
        /// Copy original HttpRequest's headers and porperties <see cref="copyRequest"/>
        /// </summary>
        /// <param name="originalRequest">a old request need to be copy to new request</param>
        /// <param name="originalResponse">a old response offers header location information to be compared with old request target URL</param>
        /// <returns>The <see cref="HttpRequestMessage"/></returns>
        /// <remarks>
        /// Copy original request's headers, properities, content and set new uri to new request from original response
        /// Remove the authorization header if authority of the location header is different with the origianl target URL
        /// </remarks>
        internal HttpRequestMessage CopyRequest(HttpRequestMessage originalRequest, HttpResponseMessage originalResponse)
        {
            var newRequest = new HttpRequestMessage(originalRequest.Method, originalRequest.RequestUri);

            foreach (var header in originalRequest.Headers)
            {
                newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            foreach (var property in originalRequest.Properties)
            {
                newRequest.Properties.Add(property);
            }

            return newRequest;
        }

        /// <summary>
        /// Check whether the request is permanently redirected by permanentRedirectCache
        /// </summary>
        /// <param name="originalRequest">The http request message</param>
        /// <returns></returns>
        private bool CheckRedirectPermanentCache(HttpRequestMessage originalRequest)
        {

            return false;
        }

        /// <summary>
        /// Checks the response's status code
        /// </summary>
        /// <param name="statusCode">The response status code <see cref="HttpStatusCode"/></param>
        /// <returns></returns>
        private bool IsRedirect(HttpStatusCode statusCode)
        {
            return (int)statusCode >= 300 && (int)statusCode < 400 && statusCode != HttpStatusCode.NotModified && statusCode != HttpStatusCode.UseProxy;
        }


    }

}
