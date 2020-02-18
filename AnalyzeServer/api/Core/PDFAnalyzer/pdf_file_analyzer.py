from app import app
from Common.constants import FILE_STATUS_MALICIOUS


class PDFFileAnalyzer:
    def __init__(self, classifier):
        self.classifier = classifier

    def process(self, filepath):
        app.logger.info("Processing file: '{}'".format(filepath))

        result = self.classifier.classify(filepath)
        return result == FILE_STATUS_MALICIOUS
