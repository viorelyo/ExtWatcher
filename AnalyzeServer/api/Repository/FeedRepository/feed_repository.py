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

    def add_event(self, event_type, filename, origin_ip):
        app.logger.info("Adding new event. Event Type: '{}'; Filename: '{}'; Origin-IP: '{}'".format(event_type, filename, origin_ip))
        server_current_time = time.strftime("%d/%m/%Y - %H:%M:%S")
        self.feed.insert({"datetime": server_current_time, "event_type": event_type.lower(), "filename": filename, "origin_ip": origin_ip})

    def get_feed(self):
        app.logger.info("Getting all events from feed.")
        return self.feed.all()
