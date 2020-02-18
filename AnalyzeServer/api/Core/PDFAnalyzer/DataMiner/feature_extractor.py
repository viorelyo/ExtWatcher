import subprocess
import re
from app import app
from Common.constants import PYTHON_INTERPRETER_NAME, PDFID_LOCATION, PDFID_FEATURES_COUNT


class FeatureExtractor:
    def __init__(self, filepath):
        self.filepath = filepath
        self.features = []
        app.logger.info("FeatureExtractor initialized.")

    def extract(self):
        app.logger.info("Extracting features for file: '{}'".format(self.filepath))

        output = self.__run_pdfid()
        if len(output) == PDFID_FEATURES_COUNT:
            self.__featurize(output)
            return self.features
        else:
            return None

    def __run_pdfid(self):
        app.logger.info("Running PDFiD for file: '{}'".format(self.filepath))

        proc = subprocess.Popen([PYTHON_INTERPRETER_NAME, PDFID_LOCATION, self.filepath], stdout=subprocess.PIPE)
        output = str(proc.communicate()[0], 'utf-8').strip()

        output = output.split("\n")[2:]     # skip PDFiD Header
        return output

    def __featurize(self, output_lines):
        """
        Extract values from output of PDFiD and featurize the pdf
        """

        app.logger.info("Featurizing file: '{}'".format(self.filepath))

        obfuscations = 0
        
        for line in output_lines:
            val = line.split()[-1]
            # Parse number of obfuscations if it exists
            m = re.match(r"(\d*)\((\d*)\)", val)
            if m:
                x = int(m.group(1))
                obfuscations += int(m.group(2))
            else:
                x = int(val)
                
            self.features.append(x)

        self.features.append(obfuscations)
