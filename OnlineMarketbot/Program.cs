using Telegram.Bot;
using Telegram;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;
using OnlineMarketbot;


namespace OnlineMarketbot
{
    class Program
    {
        static async Task Main(string[] args)
        {

           

            const string token = "6927253004:AAFwitF0MaBS2RAABgqWxIFDaBmg7rTDB8M";
            string? link;

            TelegramBotHandler handler = new TelegramBotHandler(token);

            try
            {
                await handler.BotHandle();
            }
            catch (Exception ex)
            {
                throw new Exception("No error");
            }
        }
    }
}