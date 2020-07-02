<p align="center"><img src="/Logos/2.jpg" width="250"></p>  

# ExtWatcher
Cloud based framework for detecting malicious files using Machine Learning.

## Description
`ExtWatcher` evolved from the idea of Bachelor Thesis Project. At the moment it has support just for analyzing `PDF` files. But there are plans for integrating other ML models for analyzing different file formats.  
By assembling more components into ExtWatcher, we came up with a more complex software system. We have developed:
1. `Windows Service` for detecting the downloaded files, blocking and uploading them to the Analyzing Framework. It can take the corresponding action on that blocked files based on the scanning result (unblock + keep / delete).
2. `System Tray Application` for catching the events thrown by Windows Service when a file is being scanned. The events are transformed into Windows System Notifications.
3. `Windows Installer` that wraps both the Windows Service and the System Tray App in order to automate the configuration and start of the application.
3. `React Dashboard` for visualizing the metadata generated after scanning files. As a bonus it offers a crossplatform solution that implies submitting the URL of a file and the Framework will download and analyze it automatically.

## Overview

<img src="/Paper/figures/deployTech.png" width="900">

## Built with
* C#, .NET
* Python Flask
* Scikit-learn
* ReactJS

## Links
[Thesis](./Thesis/Thesis.pdf)  
[Presentation](./Thesis/Cloud_Based_Malicious_PDF_Detection_Using_Machine_Learning.pdf)   

## Demos
- Windows Service + System Tray App

<p align="center"><img src="/Paper/figures/notification.png" width="300"></p>  

<p align="center"><img src="/Paper/figures/trayStatuses.png" width="470"></p>  


- ExtWatcher Dashboard

<img src="/Paper/figures/dashboard.gif" width="960">
