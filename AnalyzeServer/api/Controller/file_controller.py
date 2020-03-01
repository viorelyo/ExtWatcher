import os
from hashlib import md5
from app import app
from werkzeug.utils import secure_filename
from Common.constants import FILE_STATUS_BENIGN, FILE_STATUS_MALICIOUS, FILE_STATUS_UNDETECTED, UPLOAD_FOLDER, SUBMIT_REQUEST_TYPE, UPLOAD_REQUEST_TYPE


class FileController:
    def __init__(self, file_repo, feed_repo, analyzer):
        self.file_repo = file_repo
        self.feed_repo = feed_repo
        self.analyzer = analyzer

    def upload(self, file, requester_ip):
        request_type = UPLOAD_REQUEST_TYPE
        filename = self.__save_uploaded_file(file)
        self.feed_repo.add_event(request_type, filename, requester_ip)

        result = self.__analyze(filename)
        return result

    def submit(self, submitted_url, requester_ip):
        request_type = SUBMIT_REQUEST_TYPE
        filename = self.__save_file_from_url(submitted_url)
        self.feed_repo.add_event(request_type, filename, requester_ip)

        result = self.__analyze(filename)
        return result

    def get_all_analyzed_files(self):
        # time.sleep(5)   # testing UI Spinner TODO remove in production
        return self.file_repo.get_all_files()

    def get_feed(self):
        return self.feed_repo.get_feed()

    def search_analyzed_file_by_filename(self, filename):
        return self.file_repo.get_file_by_filename(filename)

    def search_analyzed_file_by_filehash(self, file_hash):
        return self.file_repo.get_file_by_filehash(file_hash)

    def __analyze(self, filename):
        filepath = os.path.join(UPLOAD_FOLDER, filename)
        file_hash = self.__get_file_hash(filepath)

        # If file is in DB it shouldn't be reanalyzed
        file_data = self.file_repo.get_file_by_filehash(file_hash)
        if file_data is None:
            self.file_repo.add_file(file_hash, filename, self.__extract_file_extension(filename))

            result = self.analyzer.process(filepath)
            result, analysis_time = self.__get_result(result)
            self.file_repo.update_file_result(file_hash, result, analysis_time)
        else:
            app.logger.info("MD5 of the file: '{}' found in DB. Returning it's result".format(filename))
            result = file_data['result']

        self.__delete_file(filepath)
        return result

    def __save_uploaded_file(self, file):
        filename = secure_filename(file.filename)
        filepath_to_be_analyzed = os.path.join(UPLOAD_FOLDER, filename)

        app.logger.info("Saving uploaded file: '{}'".format(filepath_to_be_analyzed))
        file.save(filepath_to_be_analyzed)
        return filename

    def __save_file_from_url(self, submitted_url):
        #TODO implement file download from url
        filename = ""
        return filename

    def __delete_file(self, filepath):
        app.logger.info("Removing temp saved file: '{}'".format(filepath))
        os.remove(filepath)

    def __extract_file_extension(self, filename):
        f, ext = os.path.splitext(filename)
        return ext[1:]          # remove "." from file extension
    
    def __get_file_hash(self, filepath):
        app.logger.info("Getting MD5 hash of temp saved file: '{}'".format(filepath))
        file_hash = md5()
        with open(filepath, "rb") as f:
            for chunk in iter(lambda: f.read(4096), b""):
                file_hash.update(chunk)
        return file_hash.hexdigest()

    def __get_result(self, result):
        if result['result'] is None:
            return FILE_STATUS_UNDETECTED, "N/A"
        elif result['result'] == FILE_STATUS_MALICIOUS:
            return FILE_STATUS_MALICIOUS, result['analysis_time']
        elif result['result'] == FILE_STATUS_BENIGN:
            return FILE_STATUS_BENIGN, result['analysis_time']
