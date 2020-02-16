# FLASK API

## TODO
- [ ] Identify md5/anything_else of each uploaded file, save in DB 
- [ ] General search by md5 (when uploading - forst calculate md5 - see if available then use ML)
- [ ] Use VirusTotal API + compare results
- [x] Integrate ML into API
- [x] Test API with integrated ML
- [ ] Handle possible error if file couldn't be uploaded and we don't have it's path (fileController.analyze)
- [ ] DB refactor