import subprocess
import logging
from app import app

PDFID_LOCATION = "./Utils/pdfid.py"
DEFAULT_COUNT = 10


class FeatureExtractor:
    def __init__(self, filepath):
        self.filepath = filepath
        self.features = []
        app.logger.info("FeatureExtractor initialized.")

    def run_pdfid(self):
        app.logger.info("Running pdfid.py for file: '{}'".format(self.filepath))

        output = subprocess.Popen(["python3", PDFID_LOCATION, self.filepath], stdout=subprocess.PIPE).stdout
        output = output.readlines()
        return output

    def extract(self):
        app.logger.info("Extracting features for file: '{}'".format(self.filepath))

        output = self.run_pdfid()
        if len(output) == 24:
            self.select_features(output)
            return self.features
        else:
            return None
    
    def select_features(self, output_lines):
        """
        
        """
        section_count = [] 

        obj_section = bytes(output_lines[2])
        obj_section = obj_section.replace(b'obj', b'')
        obj_section = obj_section.replace(b' ', b'')
        obj_section = obj_section.replace(b'\\n', b'')
        obj_section = obj_section.decode('UTF-8')
        try:
            obj = int(obj_section)
        except ValueError:
            obj = DEFAULT_COUNT
        finally:
            section_count.append(obj)
        

            
