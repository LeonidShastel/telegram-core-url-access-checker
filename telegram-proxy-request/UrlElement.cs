using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegram_proxy_request
{
    public class UrlElement
    {
        long chatId = 0;
        string url = string.Empty;
        Proxy proxy;

        public string Url { get => url; set => url=value; }
        public Proxy Proxy { get => proxy; set => proxy=value; }
        public long ChatId { get => chatId; set => chatId=value; }

        public UrlElement(string url, Proxy proxy, long chatId)
        {
            this.url = url;
            this.proxy = proxy;
            this.chatId=chatId;
        }
    }
}
