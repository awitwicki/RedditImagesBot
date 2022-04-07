# RedditImagesBot
Bot for posting images to telegram channels

## Install

Use next environment variables:

* `TELEGRAM_TOKEN={YOUR_TOKEN}` - telegram token

* `TELEGRAM_CHANNEL=@yourchannel` - channel name

* `REDDIT_TOPIC_URL=https://www.reddit.com/r/EarthPorn/` - link to subreddit

**Docker compose:**  create `.env` file and fill it with that variables.

## Run

```
docker-compose up --build -d
```
