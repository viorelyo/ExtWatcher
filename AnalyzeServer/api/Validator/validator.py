import requests
from urllib.parse import urlparse
from Common.constants import ALLOWED_EXTENSIONS


class Validator:
    def __init__(self):
        pass

    def allowed_file(self, filename):
        return '.' in filename and filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS

    def validate_url(self, submitted_url):
        if submitted_url == "":
            return False
        if not self.__check_url(submitted_url):
            return False
        return self.__check_connection(submitted_url)

    def __check_url(self, url):
        try:
            result = urlparse(url)
            return all([result.scheme, result.netloc, result.path])
        except:
            return False

    def __check_connection(self, url):
        try:
            request = requests.get(url)
            if request.status_code == 200:
                return True
            else:
                return False
        except ConnectionError:
            return False
