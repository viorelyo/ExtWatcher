import subprocess
import logging
from app import app

PDFID_LOCATION = "./Utils/pdfid.py"


class FeatureExtractor:
    def __init__(self, filepath):
        self.filepath = filepath
        self.features = []
        app.logger.info("FeatureExtractor initialized.")

    def run_pdfid(self):
        app.logger.info("Running pdfid.py for file: '{}'".format(self.filepath))

        output = subprocess.Popen(["python3", PDFID_LOCATION, self.filepath], stdout=subprocess.PIPE).stdout
        output = stdout.readlines()
        return output

    def extract(self):
        app.logger.info("Extracting features for file: '{}'".format(self.filepath))

        output = self.run_pdfid()
        if len(output) == 24:
            self.select_features(output)
            return self.features
        else:
            return None
    
    def select_features(self, output):
        pass

