using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace Telegram.Bot.Webhook.Command
{
    public class BotCommandHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<BotCommandHandler> _logger;

        public BotCommandHandler(ITelegramBotClient botClient, ILogger<BotCommandHandler> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }

        public async Task EchoAsync(Update update)
        {
            try
            {
                string message = update.Message.Text;
                if (message.StartsWith("/")) // this is a command
                {
                    message = message.Split(" ")[0].Replace("/", "");
                }
                else // others
                {
                    message = "option";
                }
                string classNameOnInput = char.ToUpper(message[0]) + message[1..];

                IBotCommand botCommand = CreateCommandInstance<IBotCommand>($"Telegram.Bot.Webhook.Command.{classNameOnInput}Command");
                if (botCommand != null)
                {
                    await botCommand.Execute(_botClient, update);
                }
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(exception);
            }
        }

        private T CreateCommandInstance<T>(string className)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetType(className).FullName;
            return type != null ? (T)Activator.CreateInstanceFrom(assembly.Location, type).Unwrap() : default(T);
        }

        public Task HandleErrorAsync(Exception exception)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogInformation(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
