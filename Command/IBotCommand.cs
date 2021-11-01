using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Bot.Webhook.Command
{
    public interface IBotCommand
    {
        Task Execute(ITelegramBotClient botClient, Update update);
    }
}
