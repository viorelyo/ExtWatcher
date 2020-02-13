import _pickle as cPickle
from pandas import read_csv
from sklearn.ensemble import RandomForestClassifier
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score
from sklearn.metrics import confusion_matrix
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

        self.model = RandomForestClassifier()

    def train(self):
        print("Splitting train and test data(30%)...")
        self.x_train, self.x_test, self.y_train, self.y_test = train_test_split(self.x, self.y, test_size=0.3)

        print("Training RandomForestClassifier model from the training set...")
        self.model.fit(self.x_train, self.y_train)

    def test(self):
        print("Predicting on test data...")
        y_predict = self.model.predict(self.x_test)

        acs = accuracy_score(self.y_test, y_predict)
        print("Accuracy: ", acs)

        cm = confusion_matrix(self.y_test, y_predict)
        print("Confusion Matrix:\n", cm)

    def save_model(self):
        print("Saving model...")
        with open("model.sav", 'wb') as f:
            cPickle.dump(self.model, f)
