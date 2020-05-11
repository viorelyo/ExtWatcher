import time
from app import app


class PDFFileAnalyzer:
    def __init__(self, classifier):
        self.classifier = classifier

    def process(self, filepath):
        app.logger.info("Processing file: '{}'".format(filepath))

        start_analysis_time = time.time()
        result = self.classifier.classify(filepath)
        end_analysis_time = time.time()

        total_analysis_time = end_analysis_time - start_analysis_time
        return {'result': result, 'analysis_time': total_analysis_time}
