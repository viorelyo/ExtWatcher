import subprocess
import re


PYTHON_INTERPRETER_NAME = "python"
PDFID_LOCATION = "DataMiner/Utils/pdfid.py"
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
