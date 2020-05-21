"""
    TESTING
"""
from DataMiner.dataset_creator import DatasetCreator
from Classifier.model_trainer import ModelTrainer
from Classifier.classifier import Classifier

# === Step 1: Create dataset.csv by featurizing each pdf ===
d = DatasetCreator()
d.create()

# === Step 2: Train model ===
# m = ModelTrainer()
# m.train()
# m.save_model()
# m.test()

# === Step 3: Classify files using saved model ===
# c = Classifier()
# print(c.classify(r"file.pdf"))