import os
import re
import requests
import mimetypes
from app import app


class FileUtils:
    def __init__(self):
        pass

    @staticmethod
    def extract_file_extension(filename):
        app.logger.info("Retrieving file extension from filename: '{}'".format(filename))
        f, ext = os.path.splitext(filename)
        return ext[1:]          # remove "." from file extension

    @staticmethod
    def get_file_type(submitted_url):
        app.logger.info("Retrieving filetype from submitted url: '{}'".format(submitted_url))
        response = requests.get(submitted_url)
        content_type = response.headers.get('content-type')
        extension = mimetypes.guess_extension(content_type)
        if extension is None:
            return None
        return extension[1:]        # remove "." from file extension

    @staticmethod
    def get_filename(submitted_url):
        app.logger.info("Retrieving filename from submitted url: '{}'".format(submitted_url))
        response = requests.get(submitted_url)
        cd = response.headers.get('content-disposition')
        if not cd:
            if submitted_url.find('/'):
                return submitted_url.rsplit('/', 1)[1]
        filename = re.findall('filename=(.+)', cd)[0]
        #TODO implement in case of errors
        # if len(filename) == 0:
        #     return None
        if filename[0] == '"':
            filename = re.findall('"([^"]*)"', filename)[0]
        elif filename[0] == "'":
            filename = re.findall("'([^']*)'", filename)[0]
        return filename

    @staticmethod
    def get_file_size(filepath):
        size_bytes = os.path.getsize(filepath)

        for unit in ['B','KB','MB']:
            if abs(size_bytes) < 1024.0:
                return "%3.1f %s" % (size_bytes, unit)
            size_bytes /= 1024.0
        return "%.1f %s" % (size_bytes, 'GB')
