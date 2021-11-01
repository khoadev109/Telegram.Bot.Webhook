using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Webhook.Command
{
    public class QuotesCommand : IBotCommand
    {
        private readonly string[] _quotes = {
            "Art is about trying to find the good in people and making the world a more compassionate place.",
            "The simple act of paying attention can take you a long way.",
            "Sometimes enemies are our best teachers, people can learn from their mistakes, destruction sometimes means rebirth.",
            "Grief changes shape but it never ends.",
            "Multi-culture is the real culture of the world — a pure race doesn’t exist.",
            "When I don't feel free and can't do what I want I just react. I go against it.",
            "Sometimes simple things are the most difficult things to achieve.",
            "Luxury is the opportunity to experience quality, be it a place, a person or an object.",
            "Nothing ever truly dies. The universe wastes nothing, everything is simply transformed."
        };

        public async Task Execute(ITelegramBotClient botClient, Update update)
        {
            Random rand = new();
            int index = rand.Next(_quotes.Length);
            var randomQuote = _quotes[index];

            var chatId = update.Message.Chat.Id;

            Console.WriteLine($"Received a '{update.Message.Text}' message in chat {chatId}.");

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: randomQuote
            );
        }
    }
}
