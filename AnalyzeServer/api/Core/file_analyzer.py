from app import app
from Core.Classifier.classifier import Classifier
from Common.constants import FILE_STATUS_MALICIOUS, FILE_STATUS_BENIGN


class FileAnalyzer:
    def __init__(self, filepath):
        self.filepath = filepath

    def process(self):
        app.logger.info("Processing file: '{}'".format(self.filepath))

        classifier = Classifier()
        result = classifier.classify(self.filepath)
        print(result)
        return result == FILE_STATUS_MALICIOUS