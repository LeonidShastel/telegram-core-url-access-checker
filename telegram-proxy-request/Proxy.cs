using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegram_proxy_request
{
    public class Proxy
    {
        string url = string.Empty;
        string port = string.Empty;
        string username = string.Empty;
        string password = string.Empty;

        public string Url { get => url; set => url=value; }
        public string Port { get => port; set => port=value; }
        public string Username { get => username; set => username=value; }
        public string Password { get => password; set => password=value; }

        public Proxy(string url, string port, string username, string password)
        {
            this.url=url;
            this.port=port;
            this.username=username;
            this.password=password;
        }
        public Proxy(string url, string port)
        {
            this.url=url;
            this.port=port;
        }
    }
}
