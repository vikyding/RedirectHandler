# RedirectHandler
Class Design Proposal and pseudocode for RedirectHandler class
Basic Requirements 
https://github.com/microsoftgraph/msgraph-sdk-design/blob/master/middleware/RedirectHandler.md
  1. Basic sendAsync method
  2. Handler several redirection for different 3xx status codes
  3. Redirection limitation
  4. Authoriation header remove for unneccesary requests
  5. Redirect Cache Design
     a. Permanently Redirects should be cached (308, 301)
     b. 308 and 301 have different secnarios, 301 should take care of http method
     c. some sepcic situations 
        the request returned a new url with 301, and then cached it, but the request change back after a while
        send A , get new B, cache A->B, then send B get new C, chaining redirects
        301 is only cached for GET and HEAD method

Something are not included in this design.
1. Set constant max redirects for all requests right now, allow customization later
2. Buffering requirement
3. Add disable redirect option later
