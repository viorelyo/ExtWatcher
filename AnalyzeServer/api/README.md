# FLASK API

## TODO
- [ ] IF file is not pdf - JOPA!!!!!!
- [x] Identify md5/anything_else of each uploaded file, save in DB 
- [x] General search by md5 (when uploading - forst calculate md5 - see if available then use ML)
- [ ] Use VirusTotal API + compare results
- [ ] Calculate time of analyze for each file
- [x] Integrate ML into API
- [x] Test API with integrated ML
- [x] Handle possible error if file couldn't be uploaded and we don't have it's path (fileController.analyze)
- [ ] Remove file from uploads after analysis
- [x] DB refactor
- [x] Update verdict in DB after analyzing
- [x] Rename search functions
- [x] Make methods private
- [ ] Permit multiple Types of file_analyzers
- [ ] Use correct analyzer in controller.analyze
- [ ] Add more logging in each component


## Installation
1. Folder Resources in root (see structure on disk) untracked by git
- db
- logs
- models
- uploads