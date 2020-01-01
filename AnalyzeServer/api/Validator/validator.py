from Common.constants import *


class Validator:
    def __init__(self):
        pass

    def allowed_file(self, filename):
        return '.' in filename and filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS
