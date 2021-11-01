using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Bot.Webhook.Command
{
    public class HelpCommand : IBotCommand
    {
        public async Task Execute(ITelegramBotClient botClient, Update update)
        {
            var chatId = update.Message.Chat.Id;

            Console.WriteLine($"Received a '{update.Message.Text}' message in chat {chatId}.");

            string returnText =
            @"
                List of available commands:
                    
                ""/help"": get all available commands of the bot
                ""/quotes"": get Keanu Reeves's random quotes
                ""/input"": get input parameters separated by whitespace. Example: /input param1 param2
                ""/option"": get latest exchange rates of your selected currency (AUD, USD, SGD, EUR). Base rate is EUR
            ";

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: returnText
            );
        }
    }
}
