import time
from Common.constants import DB_FEED
from app import app
from tinydb import TinyDB, Query


class FeedRepository:
    def __init__(self):
        self.db = TinyDB(DB_FEED)
        self.feed = self.db.table("feed")
        self.query = Query()
        app.logger.info("FeedRepository initialized.")

    def add_event(self, event_type, content, origin_ip):
        app.logger.info("Adding new event. Event Type: '{}'; Content: '{}'; Origin-IP: '{}'".format(event_type, content, origin_ip))
        server_current_time = time.strftime("%d/%m/%Y - %H:%M:%S")
        self.feed.insert({"datetime": server_current_time, "event_type": event_type.lower(), "content": content, "origin_ip": origin_ip})

    def get_feed(self):
        app.logger.info("Getting all events from feed.")
        a = self.feed.all()
        return sorted(a, key=lambda r: time.strptime(r['datetime'], "%d/%m/%Y - %H:%M:%S"), reverse=True)
