import os
import time
import urllib.request
from app import app
from flask import Flask, request, redirect, jsonify
from werkzeug.utils import secure_filename
from Common.constants import *


def allowed_file(filename):
	return '.' in filename and filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS


@app.route('/api/file-upload', methods=['POST'])
def upload_file():
    print(request)
    print(request.files)
    print(request.headers)
    if 'file' not in request.files:
        resp = jsonify({'message' : 'No file part in the request'})
        resp.status_code = 400
        return resp
    
    file = request.files['file']
    if file.filename == '':
        resp = jsonify({'message' : 'No file selected for uploading'})
        resp.status_code = 400
        return resp
    if file and allowed_file(file.filename):
        filename = secure_filename(file.filename)
        file.save(os.path.join(app.config['UPLOAD_FOLDER'], filename))
        time.sleep(10)
        resp = jsonify({'message' : 'benign'})
        resp.status_code = 200
        return resp
    else:
        resp = jsonify({'message' : 'Allowed file types are: pdf'})
        resp.status_code = 400
        return resp


if __name__ == "__main__":
    app.run()