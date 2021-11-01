using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Webhook.Command;

namespace Telegram.Bot.Webhook.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] BotCommandHandler botCommandHandler,
                                              [FromBody] Update update)
        {
            await botCommandHandler.EchoAsync(update);
            return Ok();
        }
    }
}