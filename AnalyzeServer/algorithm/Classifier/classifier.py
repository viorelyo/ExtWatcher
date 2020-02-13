import _pickle as cPickle
from DataMiner.feature_extractor import FeatureExtractor


MODEL_FILE = "model.sav"


class Classifier:
    def __init__(self):
        print("Loading model...")
        with open(MODEL_FILE, 'rb') as f:
            self.model = cPickle.load(f)
    
    def classify(self, filepath):
        feature_extractor = FeatureExtractor(filepath)
        features = [feature_extractor.extract()]
        if features is None:
            print("[ERROR] Could not extract features for file: '{}'".format(filepath))
            return None
        else:
            result = self.model.predict(features)
            return result



        