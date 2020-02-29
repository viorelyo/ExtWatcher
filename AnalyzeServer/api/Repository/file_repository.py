import time
import Common.constants as g
from app import app
from tinydb import TinyDB, Query
from tinydb.operations import set


class FileRepository:
    def __init__(self):
        self.db = TinyDB(g.DB_FILE)
        self.files = self.db.table("files")
        self.query = Query()
        app.logger.info("FileRepository initialized.")

    def add_file(self, file_hash, filename, filetype):
        app.logger.info("Adding new file. File-md5: '{}'; Filename: '{}'".format(file_hash, filename))
        server_current_time = time.strftime("%d/%m/%Y - %H:%M:%S")
        self.files.upsert({'file_hash': file_hash, 'filename': filename, 'filetype': filetype.lower(), 'result': 'N/A', 'datetime': server_current_time, 'analysis_time': 'N/A'}, self.query.file_hash == file_hash)
    
    def update_file_result(self, file_hash, result, analysis_time):
        app.logger.info("Updating result for file with md5: '{}'".format(file_hash))
        self.files.update({"result": result, "analysis_time": analysis_time}, self.query.file_hash == file_hash)

    def get_all_files(self):
        app.logger.info("Getting all files.")
        return self.files.all()

    def get_file_by_filename(self, filename):
        app.logger.info("Getting file by it's filename: '{}'".format(filename))
        return self.files.get(self.query.filename == filename)

    def get_file_by_filehash(self, file_hash):
        app.logger.info("Getting file by it's md5: '{}'".format(file_hash))
        return self.files.get(self.query.file_hash == file_hash)
