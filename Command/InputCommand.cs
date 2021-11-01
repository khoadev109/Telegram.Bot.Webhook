using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Bot.Webhook.Command
{
    public class InputCommand : IBotCommand
    {
        public async Task Execute(ITelegramBotClient botClient, Update update)
        {
            var chatId = update.Message.Chat.Id;

            Console.WriteLine($"Received a '{update.Message.Text}' message in chat {chatId}.");

            string[] parameters = update.Message.Text.Split(" ").Skip(1).ToArray();

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"Input parameters: {string.Join(",", parameters)}"
            );
        }
    }
}
