import time
import os
from hashlib import md5
from app import app
from werkzeug.utils import secure_filename
from Common.constants import FILE_STATUS_BENIGN, FILE_STATUS_MALICIOUS, FILE_STATUS_UNDETECTED, UPLOAD_FOLDER


class FileController:
    def __init__(self, repo, analyzer):
        self.repo = repo
        self.filepath_to_be_analyzed = None
        self.analyzer = analyzer

    def analyze(self, file):
        file_hash = self.__get_file_hash(file)

        # If file is in DB it shouldn't be saved
        file_data = self.repo.get_file_by_filehash(file_hash)

        if file_data is None:
            filename = self.__save_uploaded_file(file)
            self.repo.add_file(file_hash, filename, self.__extract_file_extension(filename))

            result = self.analyzer.process(self.filepath_to_be_analyzed)
            # TODO delete file after analysis

            result = self.__get_result(result)
            self.repo.update_file_result(file_hash, result)
            return result
        else:
            app.logger.info("MD5 of the file: '{}' found in DB. Returning it's result".format(file.filename))
            return file_data['result']

    def get_all_analyzed_files(self):
        time.sleep(5)   # testing UI Spinner TODO remove in production
        return self.repo.get_all_files()

    def search_analyzed_file_by_filename(self, filename):
        return self.repo.get_file_by_filename(filename)

    def search_analyzed_file_by_filehash(self, file_hash):
        return self.repo.get_file_by_filehash(file_hash)

    def __save_uploaded_file(self, file):
        filename = secure_filename(file.filename)
        self.filepath_to_be_analyzed = os.path.join(UPLOAD_FOLDER, filename)

        app.logger.info("Saving uploaded file: '{}'".format(self.filepath_to_be_analyzed))
        file.save(self.filepath_to_be_analyzed)
        return filename

    def __extract_file_extension(self, filename):
        f, ext = os.path.splitext(filename)
        return ext[1:]          # remove "." from file extension
    
    def __get_file_hash(self, file):
        file_hash = md5(file.read()).hexdigest()
        file.seek(0)        # rewind file pointer after .read()
        return file_hash

    def __get_result(self, result):
        if result is None:
            return FILE_STATUS_UNDETECTED
        elif result == FILE_STATUS_MALICIOUS:
            return FILE_STATUS_MALICIOUS
        elif result == FILE_STATUS_BENIGN:
            return FILE_STATUS_BENIGN
