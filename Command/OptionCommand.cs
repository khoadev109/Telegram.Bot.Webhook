using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Webhook.Configuration;

namespace Telegram.Bot.Webhook.Command
{
    public class OptionCommand : IBotCommand
    {
        public async Task Execute(ITelegramBotClient botClient, Update update)
        {
            var chatId = update.Message.Chat.Id;
            var textMessage = update.Message.Text;

            Console.WriteLine($"Received a '{textMessage}' message in chat {chatId}.");

            string[] currencies = { "AUD", "USD", "SGD", "EUR" };

            if (currencies.Contains(textMessage))
            {
                double latestRate = await GetExchangeRate(textMessage);
                await botClient.SendTextMessageAsync(
                    chatId,
                    $"Latest rate of {textMessage}: {latestRate}",
                    replyMarkup: new ReplyKeyboardRemove());
            }
            else
            {
                var options = new[] {
                        new KeyboardButton("AUD"),
                        new KeyboardButton("USD"),
                        new KeyboardButton("SGD"),
                        new KeyboardButton("EUR") };

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                     text: "Select currency to get latest exchange rate",
                     replyMarkup: new ReplyKeyboardMarkup(options) { ResizeKeyboard = true });
            }
        }

        private async Task<double> GetExchangeRate(string currency)
        {
            using var client = new HttpClient();

            var exchangeRatesConfiguration = AppData.Configuration.GetSection("ExchangeRatesConfiguration").Get<ExchangeRatesConfiguration>();

            string url = string.Format(exchangeRatesConfiguration.LatestExchangesApi, exchangeRatesConfiguration.ApiKey, currency);

            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(responseStream);
            using var jsonReader = new JsonTextReader(streamReader);

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

            ExchangeRateResponse rateResponse = serializer.Deserialize<ExchangeRateResponse>(jsonReader);

            double? rate = rateResponse.Rates?.FirstOrDefault().Value;
            return rate.GetValueOrDefault();
        }
    }

    public class ExchangeRateResponse
    {
        public bool Success { get; set; }
        public string Timestamp { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
}
