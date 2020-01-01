import logging
from flask import Flask
# from flask_cors import CORS
from Common.constants import *


app = Flask(__name__)
app.secret_key = "secret key"
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER
app.config['MAX_CONTENT_LENGTH'] = 1 * 1024 * 1024 * 1024

logging.basicConfig(filename=LOG_FILE, format='[%(asctime)s] %(levelname)s %(name)s : %(message)s', level=logging.INFO)

# CORS(app)
