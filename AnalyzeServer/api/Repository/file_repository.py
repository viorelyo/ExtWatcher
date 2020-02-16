import Common.constants as g
from app import app
from tinydb import TinyDB, Query


class FileRepository:
    def __init__(self):
        self.db = TinyDB(g.DB_FILE)
        self.files = self.db.table("files")
        self.query = Query()
        app.logger.info("FileRepository initialized.")

    def add_file(self, filename, filepath):
        app.logger.info("Adding new file. Filename: '{}'; Filepath: '{}'".format(filename, filepath))
        self.files.upsert({'filename': filename, 'filepath': filepath}, self.query.filename == filename)

    def get_all_files(self):
        app.logger.info("Getting all files.")
        return self.files.all()

    def get_file(self, filename):
        app.logger.info("Getting file by it's filename: '{}'".format(filename))
        return self.files.get(self.query.filename == filename)
