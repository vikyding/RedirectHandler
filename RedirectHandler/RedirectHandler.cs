// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RedirectHandler
{
    /**
     * 
     * */
    class RedirectHandler : DelegatingHandler
    {

        private int maxRedirects;

        /// <summary>
        /// Constructs a new RedirectHandler <see cref="RedirectHandler"/>
        /// </summary>
        /// <param name="innerHander">A Http message handler to pass to the <see cref="InnerHandler"/> for sending requests.</param>
        /// <param name="maxRedirects">A Integer value to pass to <see cref="maxRedirects"/> for limiting how many redirects are allowed for requests</param>
        public RedirectHandler(HttpMessageHandler innerHander, int maxRedirects)
        {
            if (maxRedirects < 0)
            {

            }
        }

        /// <summary>
        /// Constructs a new RedirectHandler <see cref="RedirectHandler"/> 
        /// </summary>
        /// <param name="innerHandler">A Http message handler to pass to the <see cref="InnerHandler"/> for sending requests.</param>
        public RedirectHandler(HttpMessageHandler innerHandler)
        {

        }

        public async Task<HttpResponseMessage> handlerRedirect(HttpRequestMessage httpRe) {

        }

        /// <summary>
        /// Send Request 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<HttpResponseMessage> sendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // check request whether it's permanetly redirected
                //Yes -> using cached redirection to send request
                //No  -> continue

            // send request first time to get response

            // check redirected is disable or not
                // Disabled return response 
                // Not disanled -> continue

            // check response's location not null
                //Null -> return response
                //No  -> continue

            // check whether redirect count over maxRedirects
                // Yes -> throws exception?
                // No
                    // check response status code 
                        //need to be redirected
                            // 
                            // general copy request with internal copyRequest(see copyRequest for details) Method 
                            // status code == 308 : add permanent redirection to cache 
                            // status code == 303: change request method from post to get and content to be null
                            // set buffed request body
                            // send redirect request to get reponse      
                        //no need -> return response

        }

        /// <summary>
        /// Copy original HttpRequest's headers and porperties <see cref="copyRequest"/>
        /// </summary>
        /// <param name="originalRequest">a old request need to be copy to new request</param>
        /// <param name="originalResponse">a old response offers header location information to be compared with old request target URL</param>
        /// <returns>The <see cref="HttpRequestMessage"/></returns>
        /// <remarks>
        /// Remove the authorization header if authority of the location header is different with the origianl target URL
        /// </remarks>
        internal HttpRequestMessage copyRequest(HttpRequestMessage originalRequest, HttpResponseMessage originalResponse)
        {
            var newRequest = new HttpRequestMessage(originalRequest.Method, originalRequest.RequestUri);
            newRequest.RequestUri = originalResponse.Headers.Location;

            foreach (var header in originalRequest.Headers)
            {
                newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            foreach (var property in originalRequest.Properties)
            {
                newRequest.Properties.Add(property);
            }
            if (String.Compare(newRequest.RequestUri.Host, originalRequest.RequestUri.Host, StringComparison.OrdinalIgnoreCase) != 0)
            {
                newRequest.Headers.Authorization = null;
            }
            return newRequest;
        }

        private StreamContent c
    }
}
