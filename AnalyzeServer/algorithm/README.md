# Malicious PDF Detection using Machine Learning

## What
`https://xakep.ru/2014/09/26/search-document-exploit/`
PDF Format is vulnerable. It can hide malware. 
PDF is created by objects: `PDF-файлы состоят в основном из объектов, которые бывают восьми типов: boolean-значения; числа; строки; имена (Names); массивы (упорядоченный набор объектов); словари (Dictionaries) — коллекция элементов, индексируемых по имени; потоки (Streams) — обычно содержащие большой объем данных; Null-объекты.`

`/Stream` object can contain encoded information (i.e. Javascript code)
`/JS` & `/Javascript` can be obfuscated

## How
1. Analyze structure & elements of PDF Format [4] [5]
2. Decode + Deobfuscate + Select most relevant subsections of PDF (`PDFid` does all for us)
3. `PDFiD` from PDF Tools (by Didier Stevens) for feature extraction [1] [2] [3]
4. `Pandas` for creating shuffled dataframe for supervised learning (.csv file containing features of pdfs, labeled)



### Future Work
1. `https://resources.infosecinstitute.com/analysis-malicious-documents-part-1/#article`

`https://www.slideshare.net/RhydhamJoshi/remnux-tutorial3-investigation-of-malicious-pdf-doc-documents`
**Research other malicious file types**
2. `https://blog.malwarebytes.com/threat-analysis/2013/08/the-malware-archives-pdf-files/` **Extract more features**

### Resources
1. `https://books.google.ro/books?id=iFPADwAAQBAJ&pg=PA93&lpg=PA93&dq=get+output+from+pdfid+python&source=bl&ots=AhA5MOUDSY&sig=ACfU3U1Vu-rXI6JNOqrfrddyugldu328Sg&hl=ro&sa=X&ved=2ahUKEwjZ25CK6MznAhUFmIsKHfIpBAAQ6AEwBHoECAoQAQ#v=onepage&q=get%20output%20from%20pdfid%20python&f=false` **Machine Learning for Cybersecurity Cookbook: Over 80 recipe...(Tsukerman)**
2. `https://www.question-defense.com/2012/12/25/pdfid-backtrack-5-forensics-pdf-forensics-tools-pdfid` **pdfid - Tool Description**
3. `https://blog.didierstevens.com/programs/pdf-tools/` **pdfid - Presentation + PDF Presentation**
4. `https://xakep.ru/2014/09/26/search-document-exploit/` **PDF == Malware + PDF Structure**
5. `https://resources.infosecinstitute.com/pdf-file-format-basic-structure/` **PDF Format**