function Stop-ExtWatcher {
    Stop-Process -Name "ExtWatcher Service"
    Stop-Service -Name "ExtWatcherService"
}

function Start-ExtWatcher {
    Start-Service -Name "ExtWatcherService"
    Write-Output "Service Started"
    Start-Process -FilePath "C:\Program Files\csubb\ExtWatcher\ExtWatcher Service.exe"
    Write-Output "Client Started"
}