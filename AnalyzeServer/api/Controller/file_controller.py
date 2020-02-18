import time
import os
from hashlib import md5
from app import app
from werkzeug.utils import secure_filename
from Core.file_analyzer import FileAnalyzer
from Common.constants import FILE_STATUS_BENIGN, FILE_STATUS_MALICIOUS, UPLOAD_FOLDER


class FileController:
    def __init__(self, repo):
        self.repo = repo
        self.filepath_to_be_analyzed = None
        self.analyzer = None

    def analyze(self, file):
        # Get hash of the file
        file_hash = self.get_file_hash(file)

        # If file is in DB it shouldn't be saved
        file_data = self.repo.get_file_by_filehash(file_hash)
        if file_data is None:
            filename = self.save_uploaded_file(file)
            self.repo.add_file(file_hash, filename, self.extract_file_extension(filename))

            self.analyzer = FileAnalyzer(self.filepath_to_be_analyzed)
            is_malicious = self.analyzer.process()
            # TODO delete file after analysis
            verdict = FILE_STATUS_MALICIOUS if is_malicious else FILE_STATUS_BENIGN
            self.repo.update_file_verdict(file_hash, verdict)
            return verdict
        else:
            app.logger.info("MD5 of the file: '{}' found in DB. Returning it's verdict".format(file.filename))
            return file_data['verdict']

    def save_uploaded_file(self, file):
        filename = secure_filename(file.filename)
        self.filepath_to_be_analyzed = os.path.join(UPLOAD_FOLDER, filename)

        app.logger.info("Saving uploaded file: '{}'".format(self.filepath_to_be_analyzed))
        file.save(self.filepath_to_be_analyzed)
        return filename

    def get_all_analyzed_files(self):
        time.sleep(5)   # testing UI Spinner
        return self.repo.get_all_files()

    def get_analyzed_file(self, filename):
        return self.repo.get_file_by_filename(filename)
    
    def extract_file_extension(self, filename):
        f, ext = os.path.splitext(filename)
        return ext[1:]          # remove "." from file extension
    
    def get_file_hash(self, file):
        file_hash = md5(file.read()).hexdigest()
        file.seek(0)        # rewind file pointer after .read()
        return file_hash
