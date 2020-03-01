import os
import requests
import mimetypes


class FileUtils:
    def __init__(self):
        pass

    @staticmethod
    def extract_file_extension(filename):
        f, ext = os.path.splitext(filename)
        return ext[1:]          # remove "." from file extension

    @staticmethod
    def get_file_type(submitted_url):
        response = requests.get(submitted_url)
        content_type = response.headers.get('Content-Type')
        extension = mimetypes.guess_extension(content_type)
        return extension[1:]        # remove "." from file extension
