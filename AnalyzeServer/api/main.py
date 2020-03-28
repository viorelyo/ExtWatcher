import os
from app import app
from flask import request, jsonify, send_file
from Common.constants import ALLOWED_EXTENSIONS, SERVICE_FOR_DOWNLOAD
from Common.components import file_controller, validator, file_utils


@app.route('/api/file-upload', methods=['POST'])
def upload_file():
    app.logger.info("POST Method: file-upload was triggered.\n Request: '{}';\n Headers: '{}';\n Files: '{}'"
                    .format(request, str(request.headers).strip().strip(), request.files))
    
    if 'file' not in request.files:
        response = jsonify({'message': 'No file part in the request'})
        response.status_code = 400
        return response

    file = request.files['file']
    
    if file.filename == '':
        response = jsonify({'message': 'No file selected for uploading'})
        response.status_code = 400
        return response
    elif file and validator.allowed_file(file_utils.extract_file_extension(file.filename)):
        requester_ip = request.remote_addr
        file_status = file_controller.upload(file, requester_ip)

        response = jsonify({'message': file_status})
        response.status_code = 200
        return response
    else:
        message = 'Allowed file types are: [{}]'.format(', '.join(ext for ext in ALLOWED_EXTENSIONS))

        response = jsonify({'message': message})
        response.status_code = 400
        return response


@app.route('/api/url-submit', methods=['POST'])
def submit_url():
    app.logger.info("POST Method: url-submit was triggered.\n Request: '{}';\n Headers: '{}';\n "
                    .format(request, str(request.headers).strip().strip(), ))

    submitted_url = request.form.get('url')
    if submitted_url is None:
        response = jsonify({'message': "No URL found."})
        response.status_code = 400
        return response
    if not validator.validate_url(submitted_url):
        response = jsonify({'message': "URL is not valid."})
        response.status_code = 400
        return response
    if not validator.allowed_file(file_utils.get_file_type(submitted_url)):
        message = 'Allowed file types are: [{}]'.format(', '.join(ext for ext in ALLOWED_EXTENSIONS))

        response = jsonify({'message': message})
        response.status_code = 400
        return response
    else:
        requester_ip = request.remote_addr
        file_status = file_controller.submit(submitted_url, requester_ip)

        response = jsonify({'message': file_status})
        response.status_code = 200
        return response


@app.route('/api/feed', methods=['GET'])
def get_feed():
    app.logger.info("GET Method: feed was triggered.")
    feed_events = file_controller.get_feed()

    response = jsonify({'feed': feed_events})
    response.status_code = 200
    return response


@app.route('/api/analyzed-files', methods=['GET'])
def get_files():
    app.logger.info("GET Method: analyzed-files was triggered.")
    files = file_controller.get_all_analyzed_files()

    response = jsonify({'files': files})
    response.status_code = 200
    return response


@app.route('/api/search-files', methods=['GET'])
def get_file_by_name():
    app.logger.info("GET Method: search-files was triggered.")

    search_query = request.args.get('search_query')
    if search_query:
        result = file_controller.search_by_query(search_query)

        if result is None:
            response = jsonify({'result': "Invalid query"})
            response.status_code = 200
        else:
            response = jsonify({'result': result})
            response.status_code = 200
        return response
    else:
        response = jsonify({"message": "Request does not contain filename parameter."})
        response.status_code = 400
        return response


@app.route('/download/service.zip')
def download_service():
    path = os.path.join(app.config['DOWNLOAD_FOLDER'], SERVICE_FOR_DOWNLOAD)
    return send_file(path, as_attachment=True)


if __name__ == "__main__":
    # app.run(debug=True)
    app.run(host= '0.0.0.0', debug=False)
