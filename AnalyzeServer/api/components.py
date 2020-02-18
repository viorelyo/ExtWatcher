from Controller.file_controller import FileController
from Repository.file_repository import FileRepository
from Validator.validator import Validator
from Core.PDFAnalyzer.pdf_file_analyzer import PDFFileAnalyzer
from Core.PDFAnalyzer.Classifier.classifier import Classifier as PDFClassifier


repo = FileRepository()
validator = Validator()

# Classifiers
pdf_classifier = PDFClassifier()

# Analyzers
analyzer = PDFFileAnalyzer(pdf_classifier)

file_controller = FileController(repo, analyzer)
