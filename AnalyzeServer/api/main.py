from app import app
from flask import request, jsonify
from Common.constants import *
from Validator.validator import Validator
from Controller.file_controller import FileController


@app.route('/api/file-upload', methods=['POST'])
def upload_file():
    app.logger.info("POST Method: file-uplod was triggered.\n Request: '{}';\n Headers: '{}';\n Files: '{}'"
                    .format(request, str(request.headers).strip().strip(), request.files))
    
    if 'file' not in request.files:
        response = jsonify({'message': 'No file part in the request'})
        response.status_code = 400
        return response

    file = request.files['file']
    validator = Validator()
    
    if file.filename == '':
        response = jsonify({'message': 'No file selected for uploading'})
        response.status_code = 400
        return response
    elif file and validator.allowed_file(file.filename):
        file_controller = FileController(file)
        file_controller.save_uploaded_file()
        file_status = file_controller.analyze()

        response = jsonify({'message': file_status})
        response.status_code = 200
        return response
    else:
        message = 'Allowed file types are: [{}]'.format(', '.join(ext for ext in ALLOWED_EXTENSIONS))

        response = jsonify({'message': message})
        response.status_code = 400
        return response


if __name__ == "__main__":
    app.run(debug=True)