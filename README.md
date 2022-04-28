# RedditImagesBot
Bot for posting images to telegram channels

![Tests](https://img.shields.io/github/languages/top/awitwicki/RedditImagesBot)
![Tests](https://img.shields.io/github/last-commit/awitwicki/RedditImagesBot)
![Tests](https://github.com/awitwicki/RedditImagesBot/actions/workflows/dotnet.yml/badge.svg)

## Install

Use next environment variables:

* `TELEGRAM_TOKEN={YOUR_TOKEN}` - telegram token

* `TELEGRAM_CHANNEL=@yourchannel` - channel name

* `REDDIT_TOPIC_URL=https://www.reddit.com/r/EarthPorn/` - link to subreddit

* `CRON_UTC_STRING=0 15 * * *` - cron UTC string to trigger service to post photo (every day at 15:00)


**Docker compose:** create `.env` file and fill it with that variables.

## Run

```
docker-compose up --build -d
```
