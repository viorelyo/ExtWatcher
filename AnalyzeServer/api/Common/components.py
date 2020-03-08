from Controller.file_controller import FileController
from Repository.FeedRepository.feed_repository import FeedRepository
from Repository.FileRepository.file_repository import FileRepository
from Utils.file_utils import FileUtils
from Utils.query_parser import QueryParser
from Validator.validator import Validator
from Core.PDFAnalyzer.pdf_file_analyzer import PDFFileAnalyzer
from Core.PDFAnalyzer.Classifier.classifier import Classifier as PDFClassifier


file_utils = FileUtils()
validator = Validator()
query_parser = QueryParser()

file_repo = FileRepository()
feed_repo = FeedRepository()

# Classifiers
pdf_classifier = PDFClassifier()

# Analyzers
analyzer = PDFFileAnalyzer(pdf_classifier)

file_controller = FileController(file_repo, feed_repo, analyzer, file_utils, query_parser)
