# TelegramBot interview Technical Challenge

The intention of this challenge is to see your code style and structure and approach to a basic but fun set of requirements. Feel free to inject some personality ;)

## Instructions

- copy this readme and add to new github repo (private)
- build your solution and add any notes to the readme you like (can also add some screenshots)
- deploy your bot and add the handle to the readme
- when you're ready, add me to your repository as a colaborator and I will review
- we can then schedule a call to go through your solution and approach

## The Challenge

To create a basic telegram webhook bot in c# __without__ using any bot frameworks (e.g. MicrosoftBotFramwork). You can however use a library wrapping the telegram API (e.g. https://github.com/TelegramBots/Telegram.Bot)

The bot should:
- list all it's available commands when issued the /help or /about commands
- have a command which returns random quotes from your favorite TV character
- have a command which takes parameters 
- have a command which sends back options in button form
- have a command which then awaits typed options from subsequent messages (e.g. /quotes which then accepts cryptocurrency pairs and returns the quote for latest price from an api)
- at least one command which calls an external API

As you can see there is an opportunity to satisfy the last 3 requirements in a single command if you wish.

## Telegram Webhook Bot

Following the source code here: https://github.com/TelegramBots/Telegram.Bot.Examples/tree/master/Telegram.Bot.Examples.WebHook but have some changes to adapt the requirement.

### Ngrok Setup:
Ngrok is used to create a public URL for webhook to listen for changes from input messages.

Steps:

- Download https://ngrok.com/download
- Unzip ngrok file and run ngrok.exe
- Run: ```ngrok token: 20EX7YrToXSSkN4eH6Ew8q6DS6L_2kvEZqDLUQrimDwAcQfkv``` to save the token to config file.
- Run ```tskill /A ngrok``` to end all ngrok ports running.
- Run: ```ngrok http 88```. The port 88 is also used in Telegram.Bot.Webhook project to run locally so that it can listen to the changes from public URL.
- Go to http://localhost:4040/inspect/http to get the public URL (the one with https) and also inspect the request coming to Webhook URL when user inputs message on Telegram.
- Sometime ngrok server will be down and get the error ```ERR_NGROK_802``` when run "ngrok" commands. I just faced this error some times but after waiting for some hours, it is back and work normally.

### Telegram.Bot.Webhook project:
- Create a bot from Telegram https://core.telegram.org/bots#6-botfather or use the existing one: KhoaTest
- Some settings required for project to run (change in **appsettings.Development.json**):
  + If creating new bot, then update the value for key BotConfiguration/BotToken with the token of new bot.
  + Update the value of key BotConfiguration/HostAddress to the public URL (the one with https) from http://localhost:4040/inspect/http. If there are a lot of requests displaying, just click "Clear" button to clear request logs and the public URL will appear.

### Bot:
- There are 4 available commands:
  + ```/help```: to list all available commands of this bot
  + ```/quotes```: to get Keanu Reeves's random quotes
  + ```/input```: get input parameters separated by whitespace and return parameters separated by commad. Example: /input param1 param2
  + ```/option```: get latest exchange rates of your selected currency (AUD, USD, SGD, EUR). Base rate is EUR. This command I used a free exchange rates api https://exchangeratesapi.io/ to get rate for a specific currency
 
