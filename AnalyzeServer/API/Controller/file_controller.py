import os
from urllib.request import urlretrieve
from hashlib import md5
from app import app
from werkzeug.utils import secure_filename
from Common.constants import FILE_STATUS_BENIGN, FILE_STATUS_MALICIOUS, FILE_STATUS_UNDETECTED, UPLOAD_FOLDER, SUBMIT_REQUEST_TYPE, UPLOAD_REQUEST_TYPE


class FileController:
    def __init__(self, file_repo, feed_repo, analyzer, file_utils, query_parser):
        self.file_utils = file_utils
        self.file_repo = file_repo
        self.feed_repo = feed_repo
        self.query_parser = query_parser
        self.analyzer = analyzer

    def upload(self, file, requester_ip):
        request_type = UPLOAD_REQUEST_TYPE
        filename = self.__save_uploaded_file(file)
        self.feed_repo.add_event(request_type, filename, requester_ip)

        result = self.__analyze(filename)
        return result

    def submit(self, submitted_url, requester_ip):
        app.logger.info("Retrieving filename from submitted url: '{}'".format(submitted_url))
        request_type = SUBMIT_REQUEST_TYPE
        filename = self.__save_file_from_url(submitted_url)
        self.feed_repo.add_event(request_type, submitted_url, requester_ip)

        result = self.__analyze(filename)
        return result

    def get_all_analyzed_files(self):
        return self.file_repo.get_all_files()

    def get_feed(self):
        return self.feed_repo.get_feed()

    def search_by_query(self, search_query):
        parsed_content = self.query_parser.parse(search_query)
        if parsed_content is None:
            return None

        if parsed_content["keyword"] == "filename":
            return self.file_repo.get_files_by_filename(parsed_content["param"])
        elif parsed_content["keyword"] == "hash":
            return self.file_repo.get_files_by_filehash(parsed_content["param"])
        elif parsed_content["keyword"] == "datetime":
            return self.file_repo.get_files_by_datetime(parsed_content["param"])
        elif parsed_content["keyword"] == "type":
            return self.file_repo.get_files_by_filetype(parsed_content["param"])
        elif parsed_content["keyword"] == "result":
            return self.file_repo.get_files_by_result(parsed_content["param"])

    def search_analyzed_file_by_filehash(self, file_hash):
        return self.file_repo.get_file_by_filehash(file_hash)

    def __analyze(self, filename):
        app.logger.info("Analyzing file: '{}'".format(filename))
        filepath = os.path.join(UPLOAD_FOLDER, filename)
        file_hash = self.__get_file_hash(filepath)

        # If file is in DB it shouldn't be reanalyzed
        file_data = self.file_repo.get_file_by_filehash(file_hash)
        if file_data is None:
            self.file_repo.add_file(file_hash, filename, self.file_utils.extract_file_extension(filename), self.file_utils.get_file_size(filepath))

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
        filepath = os.path.join(UPLOAD_FOLDER, filename)

        app.logger.info("Saving uploaded file: '{}'".format(filepath))
        file.save(filepath)
        return filename

    def __save_file_from_url(self, submitted_url):
        filename = self.file_utils.get_filename(submitted_url)
        filepath = os.path.join(UPLOAD_FOLDER, filename)

        app.logger.info("Saving file: '{}' from submitted url: '{}'".format(filepath, submitted_url))
        urlretrieve(submitted_url, filepath)
        return filename

    def __delete_file(self, filepath):
        app.logger.info("Removing temp saved file: '{}'".format(filepath))
        os.remove(filepath)
    
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
