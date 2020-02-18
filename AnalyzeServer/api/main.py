from app import app
from flask import request, jsonify
from Common.constants import ALLOWED_EXTENSIONS
from Validator.validator import Validator
from Controller.file_controller import FileController
from Repository.file_repository import FileRepository


repo = FileRepository()


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
        file_controller = FileController(repo)
        file_status = file_controller.analyze(file)

        response = jsonify({'message': file_status})
        response.status_code = 200
        return response
    else:
        message = 'Allowed file types are: [{}]'.format(', '.join(ext for ext in ALLOWED_EXTENSIONS))

        response = jsonify({'message': message})
        response.status_code = 400
        return response


@app.route('/api/analyzed-files', methods=['GET'])
def get_files():
    app.logger.info("GET Method: analyzed-files was triggered.")
    file_controller = FileController(repo)
    files = file_controller.get_all_analyzed_files()

    response = jsonify({'files': files})
    response.status_code = 200
    return response


@app.route('/api/analyzed-file', methods=['GET'])
def get_file_by_name():
    app.logger.info("GET Method: analyzed-file was triggered.")

    filename = request.args.get('filename')
    if filename:
        file_controller = FileController(repo)
        file = file_controller.get_analyzed_file(filename)

        if file:
            response = jsonify({'file': file})
            response.status_code = 200
        else:
            response = jsonify({"message": "Filename not found."})
            response.status_code = 404
        return response
    else:
        response = jsonify({"message": "Request does not contain filename parameter."})
        response.status_code = 400
        return response


if __name__ == "__main__":
    app.run(debug=True)
