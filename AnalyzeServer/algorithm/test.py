"""
    TESTING
"""
from Classifier.model_trainer import ModelTrainer
from Classifier.classifier import Classifier


# m = ModelTrainer()
# m.train()
# m.save_model()
# m.test()

c = Classifier()
print(c.classify(r"C:\Users\viorel\Desktop\NewFolder\maliciouspdf\Contract-18_09_2018 0_00_00.pdf"))