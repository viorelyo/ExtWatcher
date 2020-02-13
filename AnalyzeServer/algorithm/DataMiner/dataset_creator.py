import os
from pandas import DataFrame
from threading import Thread
from DataMiner.feature_extractor import FeatureExtractor


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
        benign_dataset = []
        malicious_dataset = []
        b_thread = Thread(target=self.featurize, args=(benign_dataset, "benign", self.benign_files))
        m_thread = Thread(target=self.featurize, args=(malicious_dataset, "malicious", self.malicious_files))
        
        b_thread.start()
        m_thread.start()

        b_thread.join()
        m_thread.join()

        self.dataset = benign_dataset + malicious_dataset

        # Create DataFrame from dataset 
        df = DataFrame(self.dataset)
        df.columns = FEATURES

        # Apply min-max normalization on all features excepting "Class" feature
        normalized_df = df[FEATURES[:-1]]  
        self.dataframe = (normalized_df - normalized_df.min()) / (normalized_df.max() - normalized_df.min())
        self.dataframe[FEATURES[-1]] = df[FEATURES[-1]]  
        
        # Shuffle rows 
        self.dataframe.sample(frac=1)

        # Export dataframe to CSV
        self.dataframe.to_csv('dataset.csv', index=False)

    def featurize(self, dataset, label, files):
        print("Extracting features for {} files...".format(label))
        for filename in os.listdir(files):
            file_path = os.path.join(files, filename)
            feature_extractor = FeatureExtractor(file_path)
            features = feature_extractor.extract()
            if features is None:
                print("[ERROR] Could not extract features for {} file: '{}'".format(label, filename))
            else:
                features.append(label)
                dataset.append(features)