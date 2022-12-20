using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace telegram_proxy_request
{
    public static class UrlChecker
    {
        public static async Task<bool> SendRequest(UrlElement element)
        {
            try
            {
                var proxy = new WebProxy
                {
                    Address = new Uri($"http://{element.Proxy.Url}:{element.Proxy.Port}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,

                    // *** These creds are given to the proxy server, not the web server ***
                    Credentials = new NetworkCredential(
                        userName: element.Proxy.Username,
                       password: element.Proxy.Password)
                };

                // Now create a client handler which uses that proxy
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy,
                };

                // Finally, create the HTTP client object
                var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                var response = await client.GetAsync(element.Url);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
