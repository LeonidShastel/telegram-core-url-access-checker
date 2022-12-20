

using Telegram.Bot;
using Telegram.Bot.Types;
using telegram_proxy_request;

namespace telegram_proxy_request
{
    public class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient botClient = new TelegramBotClient("5444371933:AAGaeeVKZeGlkmMQo1ENEI0XKP_m6eGCCSU");
            botClient.StartReceiving(Update, Error);
            Console.WriteLine("Bot started");
            new Thread(() => Listener.Start(botClient)).Start();
            Console.WriteLine("Listener started");
            Console.ReadLine();
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.Message is not { } message)
                    return;
                if (message.Text is not { } messageText)
                    return;

                if (message.Text == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Проверка сайта на доступность\nВведите сайт в формате:\n" +
                        "proxyIp:proxyPort:proxyUsername:proxyPassword - https://google.com");
                    return;
                }

                string[] messageInfo = message.Text.Split(" - ");
                string[] proxy = messageInfo[0].Split(":");
                if (messageInfo.Length != 2 || proxy.Length != 4)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Неверный формат");
                    return;
                }

                UrlElement element = new UrlElement(
                        messageInfo[1],
                        new Proxy(proxy[0], proxy[1], proxy[2], proxy[3]),
                        message.Chat.Id
                    );

                if (await UrlChecker.SendRequest(element))
                {
                    await botClient.SendTextMessageAsync(element.ChatId, $"Сайт {element.Url} доступен и добавлен на прослушивание", null, null, false);
                    Listener.urlsElements.Add(element);
                }
                else
                    await botClient.SendTextMessageAsync(element.ChatId, $"Сайт {element.Url} заблокирован", null, null, false);

                return;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}