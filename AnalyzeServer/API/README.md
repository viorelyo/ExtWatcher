# FLASK API

## TODO
- [ ] Add support for submitting url of pdf - download, analyze, send result
- [ ] Permit multiple Types of file_analyzers
- [ ] Use correct analyzer in controller.analyze
- [ ] Test new added type
- [x] Implement Validator.validate_url
- [x] Detect Requester's IP
- [x] Save feed into DB
- [x] Remove file from uploads after analysis
- [x] Calculate time of analyze for each file
- [x] Add current timestamp into db - when file was added
- [x] Identify md5/anything_else of each uploaded file, save in DB
- [x] General search by md5 (when uploading - forst calculate md5 - see if available then use ML)
- [x] Integrate ML into API
- [x] Test API with integrated ML
- [x] Handle possible error if file couldn't be uploaded and we don't have it's path (fileController.analyze)
- [x] DB refactor
- [x] Update result in DB after analyzing
- [x] Rename search functions
- [x] Make methods private
- [ ] Pagination?!
- [ ] Use VirusTotal API + compare results
- [ ] Add more logging in each component


## Installation
1. Folder Resources in root (see structure on disk) untracked by git
- db
- logs
- models
- uploads

2. Requirements
- requests
- sklearn