import time
import os
from app import app
from werkzeug.utils import secure_filename
from Core.file_analyzer import FileAnalyzer
from Common.constants import FILE_STATUS_BENIGN, FILE_STATUS_MALICIOUS, UPLOAD_FOLDER


class FileController:
    def __init__(self, repo):
        self.repo = repo
        self.filepath_to_be_analyzed = None
        self.analyzer = None

    def save_uploaded_file(self, file):
        filename = secure_filename(file.filename)
        self.filepath_to_be_analyzed = os.path.join(UPLOAD_FOLDER, filename)

        app.logger.info("Saving uploaded file: '{}'".format(self.filepath_to_be_analyzed))
        file.save(self.filepath_to_be_analyzed)
        self.repo.add_file(filename, self.filepath_to_be_analyzed)

    def analyze(self):
        if self.filepath_to_be_analyzed:
            self.analyzer = FileAnalyzer(self.filepath_to_be_analyzed)

            is_malicious = self.analyzer.process()
            return FILE_STATUS_MALICIOUS if is_malicious else FILE_STATUS_BENIGN
        else:
            return None

    def get_all_analyzed_files(self):
        time.sleep(5)
        return self.repo.get_all_files()

    def get_analyzed_file(self, filename):
        return self.repo.get_file(filename)
