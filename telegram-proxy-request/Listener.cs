using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace telegram_proxy_request
{
    public static class Listener
    {
        public static List<UrlElement> urlsElements = new List<UrlElement>();
        public static async void Start(TelegramBotClient client)
        {
            while (true)
            {
                try
                {
                    bool send = true;        
                    //if(DateTime.Now.Hour == 19)
                    //    send = true;
                    foreach(UrlElement urlElement in urlsElements)
                    {
                        bool result = await UrlChecker.SendRequest(urlElement);

                        if (!result)
                        {
                            try
                            {
                                await client.SendTextMessageAsync(urlElement.ChatId, $"Сайт {urlElement.Url} заблокирован");
                                urlsElements.Remove(urlElement);
                            }
                            catch(Exception ex) { Console.WriteLine(ex.Message); }
                        }
                        else if(result && send)
                        {
                            try
                            {
                                await client.SendTextMessageAsync(urlElement.ChatId, $"Сайт {urlElement.Url} доступен");
                            }
                            catch(Exception ex) { Console.WriteLine(ex.Message); }
                        }


                        Console.WriteLine($"{DateTime.Now.Hour} : {urlElement.Url} : {send}: {result}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally 
                {
                    Thread.Sleep(3600000);
                }
            }
        }
    }
}
