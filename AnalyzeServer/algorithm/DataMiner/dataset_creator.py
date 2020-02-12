import os
from pandas import DataFrame
from feature_extractor import FeatureExtractor


DATASET_PATH = r"C:\Users\viorel\Desktop\NewFolder"
BENIGN_DIR = "cleanpdf"
MALICIOUS_DIR = "maliciouspdf"
FEATURES = ['obj', 'endobj', 'stream', 'endstream', 'xref', 'trailer', 'startxref', 'Page', 'Encrypt',
            'ObjStm', 'JS', 'Javascript', 'AA', 'OpenAction', 'AcroForm', 'JBIG2Decode', 'RichMedia',
            'Launch', 'EmbeddedFile', 'XFA', 'Colors', 'Class']


class DatasetCreator:
    def __init__(self):
        self.dataset = []
        self.dataframe = None
        self.benign_files = os.path.join(DATASET_PATH, BENIGN_DIR)
        self.malicious_files = os.path.join(DATASET_PATH, MALICIOUS_DIR)

    def create(self):
        print("Extracting features for benign files...")
        for filename in os.listdir(self.benign_files):
            file_path = os.path.join(self.benign_files, filename)
            feature_extractor = FeatureExtractor(file_path)
            features = feature_extractor.extract()
            if features is None:
                print("[ERROR] Could not extract features for file: '{}'".format(filename))
            else:
                self.labelize(features, "benign")

        print("====")

        print("Extracting features for malicious files...")
        for filename in os.listdir(self.malicious_files):
            file_path = os.path.join(self.malicious_files, filename)
            feature_extractor = FeatureExtractor(file_path)
            features = feature_extractor.extract()
            if features is None:
                print("[ERROR] Could not extract features for file: '{}'".format(filename))
            else:
                self.labelize(features, "malicious")
        
        # Create DataFrame from dataset and then shuffle rows
        self.dataframe = DataFrame(self.dataset).sample(frac=1)
        self.dataframe.columns = FEATURES

        # Export dataframe to CSV
        self.dataframe.to_csv('dataset.csv', index=False)

    def labelize(self, features, label):
        features.append(label)
        self.dataset.append(features)


"""
    TEST SECTION 
"""
d = DatasetCreator()
d.create()  