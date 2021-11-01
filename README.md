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

