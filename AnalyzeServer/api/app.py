import logging
from flask import Flask
# from flask_cors import CORS
from Common.constants import UPLOAD_FOLDER, LOG_FILE


app = Flask(__name__)
app.secret_key = "secret key"
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER
app.config['MAX_CONTENT_LENGTH'] = 1 * 1024 * 1024 * 1024
# CORS(app)

logging.basicConfig(filename=LOG_FILE, format='[%(asctime)s] %(levelname)s %(name)s : %(message)s', level=logging.DEBUG)
