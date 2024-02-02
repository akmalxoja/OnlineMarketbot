using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using System.Threading;
using File = System.IO.File;
using Telegram.Bot.Types.ReplyMarkups;
using System.ComponentModel.Design;

namespace BotPractice3
{
    public class TelegramBotHandler
    {
        public string Token { get; set; }
        public object currenttime = DateTime.Now.ToString("HH:mm");

        public TelegramBotHandler(string token)
        {
            this.Token = token;
        }

        public async Task BotHandle()
        {
            var botClient = new TelegramBotClient($"{this.Token}");

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
                );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();

        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            
            var chatId = message.Chat.Id;
            
            
            if(chatId == 5513617690)
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                    new KeyboardButton[] { "Telefon", "Statistika" },
                    new KeyboardButton[] { "Office", "Mojozlar ro'yxati" },
                    new KeyboardButton[] { "Orderstatus", "Mojozlar ro'yxati" },
                })
                {
                    ResizeKeyboard = true
                };

                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Choose a response",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);




                string filepath = @"C:\Users\VICTUS\Desktop\DotNet\OnlineMarketbot\users.txt";
                var user_message = $"Received a '{messageText}' message in chat {chatId}. UserName =>  {message.Chat.Username} at {currenttime}\n";
                File.AppendAllText(filepath, user_message);

                if (message.Text == "/usd")
                {
                    var money = CenvertionMoney.Convertion(CenvertionMoney.ConnectWithJson(), "USD");
                    Message sendMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"{money} sums",
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken
                        );
                }
                else if (message.Text == "/eur")
                {
                    var money = CenvertionMoney.Convertion(CenvertionMoney.ConnectWithJson(), "EUR");
                    Message sendMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"{money} sums",
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken
                        );
                }
                else if (message.Text == "/aed")
                {
                    var money = CenvertionMoney.Convertion(CenvertionMoney.ConnectWithJson(), "AED");
                    Message sendMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"{money} sums",
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken
                        );
                }
                else if (message.Text == "/chf")
                {
                    var money = CenvertionMoney.Convertion(CenvertionMoney.ConnectWithJson(), "CHF");
                    Message sendMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"{money} sums",
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken
                        );
                }
                else if (message.Text == "/gbp")
                {
                    var money = CenvertionMoney.Convertion(CenvertionMoney.ConnectWithJson(), "GBP");
                    Message sendMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"{money} sums",
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken
                        );
                }
                else if (message.Text == "/rub")
                {
                    var money = CenvertionMoney.Convertion(CenvertionMoney.ConnectWithJson(), "RUB");
                    Message sendMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"{money} sums",
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken
                        );
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "You said: " + messageText + $" at {currenttime}\n",
                        cancellationToken: cancellationToken
                        );
                }

                
            }
            else
            {
                /*var replyKeyboard = new ReplyKeyboardMarkup(new[]
                {
                new[]
                {
                    KeyboardButton.WithRequestContact("CONTACT"),

                },

                });
                    replyKeyboard.ResizeKeyboard = true;
                    replyKeyboard.OneTimeKeyboard = true;


                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Share your contact",
                        replyMarkup: replyKeyboard,
                        cancellationToken: cancellationToken);
            

            string filepath = @"C:\Users\VICTUS\Desktop\DotNet\OnlineMarketbot\users.txt";
            var user_message = $"Received a '{messageText}' message in chat {chatId}. UserName =>  {message.Chat.Username} at {currenttime}\n";
            File.AppendAllText(filepath, user_message);

*/

                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                    new KeyboardButton[] { "Telefonlar ro'yxati", "Savatchaga qo'shish" },
                    new KeyboardButton[] { "Savatchani ko'rish", "Savatchani tozalash" },
                    new KeyboardButton[] { "Buyurtma qilish", "Mojozlar ro'yxati" },
                })
                {
                    ResizeKeyboard = true
                };

                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Choose a response",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);

            }


        }


        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
