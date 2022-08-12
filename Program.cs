using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;

namespace ICLMonitoring
{
    class Program
    {

        static ITelegramBotClient bot = new TelegramBotClient("5336520304:AAFC8OTXjYwXEApDQKftTWwvu_NDsG80rzs");
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {


            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                if (message.Text == "/start")
                {
                    ReplyKeyboardMarkup keyboard = new(new[]
                    {
                        new KeyboardButton[] { "Оборудование", "Карта" },
                        new KeyboardButton[] { "Поддержка", "Аварии" }
                    })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, text: "Choose: ", replyMarkup: keyboard);
                    return;
                }

                

                if (message.Text == "Карта")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Сыграем?");
                    return;
                }

                if (message.Text == "Оборудование")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "У вас установалены 3 машины для полива:");
                    return;
                }

                if (message.Text == "Поддержка")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Сыграем?");
                    return;
                }

                if (message.Text == "Аварии")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Сыграем?");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat.Id, text: $"Команда <{message.Text}> отсутствует в боте((( \n Если у вас есть предложения для развития бота, то напишите в поддержку");

            }


            if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                await HandleCallBackQuery(botClient, update.CallbackQuery);
                return;
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        public static async Task HandleCallBackQuery(ITelegramBotClient botClient , CallbackQuery callbackQuery)
        {

        }


        static void Main(string[] args)
        {
            //Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            Console.WriteLine("Бот запущен");
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }


       
    }




}

