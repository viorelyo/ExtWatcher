import subprocess
import logging

PYTHON_INTERPRETER_NAME = "python"
PDFID_LOCATION = "./Utils/pdfid.py"
DEFAULT_COUNT = 10
PDFID_FEATURES_COUNT = 21


class FeatureExtractor:
    def __init__(self, filepath):
        self.filepath = filepath
        self.features = []

    def run_pdfid(self):
        proc = subprocess.Popen([PYTHON_INTERPRETER_NAME, PDFID_LOCATION, self.filepath], stdout=subprocess.PIPE)
        output = str(proc.communicate()[0], 'utf-8').strip()
        output = output.split("\n")[2:]     # skip PDFiD Header
        return output

    def extract(self):
        output = self.run_pdfid()
        if len(output) == PDFID_FEATURES_COUNT:
            self.featurize(output)
            return self.features
        else:
            return None
    
    def featurize(self, output_lines):
        """
        Extract values from output of PDFiD and featurize the pdf
        """
        
        for line in output_lines:
            x = int(line.split()[-1])
            self.features.append(x)
