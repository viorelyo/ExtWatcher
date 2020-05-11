import _pickle as cPickle
from app import app
from Core.PDFAnalyzer.DataMiner.feature_extractor import FeatureExtractor
from Common.constants import MODEL_FILE


class Classifier:
    def __init__(self):
        app.logger.info("Loading model...")
        with open(MODEL_FILE, 'rb') as f:
            self.model = cPickle.load(f)
    
    def classify(self, filepath):
        app.logger.info("Classifying file: '{}'".format(filepath))

        feature_extractor = FeatureExtractor(filepath)
        features = [feature_extractor.extract()]
        if features[0] is None:
            app.logger.info("[ERROR] Could not extract features for file: '{}'".format(filepath))
            return None
        else:
            result = self.model.predict(features)[0]
            app.logger.info("Classifier predicted file: '{}' as '{}'".format(filepath, result))
            return result
