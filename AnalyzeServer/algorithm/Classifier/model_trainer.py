import time
import _pickle as cPickle
from pandas import read_csv
from sklearn.ensemble import RandomForestClassifier
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score
from sklearn.metrics import recall_score
from sklearn.metrics import precision_score
from sklearn.metrics import f1_score
from sklearn.metrics import confusion_matrix
from sklearn.metrics import classification_report
from DataMiner.dataset_creator import FEATURES


DATASET_FILE = r"C:\Users\viorel\Desktop\Bachelor\Bachelor-Ideas\AnalyzeServer\algorithm\pdfdataset_n.csv"


class ModelTrainer:
    def __init__(self):
        df = read_csv(DATASET_FILE)
        self.x = df[FEATURES[:-1]]
        self.y = df[FEATURES[-1]]

        self.x_train = None
        self.x_test = None
        self.y_train = None
        self.y_test = None

        self.model = RandomForestClassifier(n_estimators=100, n_jobs=-1)

    def train(self):
        print("Splitting train and test data(30%)...")
        self.x_train, self.x_test, self.y_train, self.y_test = train_test_split(self.x, self.y, test_size=0.3)

        print("Training RandomForestClassifier model from the training set...")
        start_time = time.time()
        self.model.fit(self.x_train, self.y_train)
        end_time = time.time()
        print("Time spent on training: ", end_time - start_time)

    def test(self):
        print("Predicting on test data...")
        start_time = time.time()
        y_predict = self.model.predict(self.x_test)
        end_time = time.time()
        print("Time spent on predicting: ", end_time - start_time)

        cm = confusion_matrix(self.y_test, y_predict)
        print("Confusion Matrix:\n", cm)

        acs = accuracy_score(self.y_test, y_predict)
        print("Accuracy: ", acs)

        rs = recall_score(self.y_test, y_predict)
        print("Recall: ", acs)

        ps = precision_score(self.y_test, y_predict)
        print("Precision: ", acs)

        f1s = f1_score(self.y_test, y_predict)
        print("F1-Score: ", acs)

        metrics = classification_report(self.y_test, y_predict)
        print("Metrics:\n", metrics)

    def save_model(self):
        print("Saving model...")
        with open("model.sav", 'wb') as f:
            cPickle.dump(self.model, f)
