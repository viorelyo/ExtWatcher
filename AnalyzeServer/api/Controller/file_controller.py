import time
import os
from werkzeug.utils import secure_filename
from Core.file_analyzer import FileAnalyzer
from Common.constants import *


class FileController:
    def __init__(self, file):
        self.file = file
        self.file_path_to_be_analyzed = None
        self.analyzer = None

    def save_uploaded_file(self):
        filename = secure_filename(self.file.filename)
        self.file_path_to_be_analyzed = os.path.join(UPLOAD_FOLDER, filename)
        self.file.save(self.file_path_to_be_analyzed)
        time.sleep(10)

    def analyze(self):
        if self.file_path_to_be_analyzed:
            self.analyzer = FileAnalyzer(self.file_path_to_be_analyzed)

        is_malicious = self.analyzer.process()
        return FILE_STATUS_MALICIOUS if is_malicious else FILE_STATUS_BENIGN
