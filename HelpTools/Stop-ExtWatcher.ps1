function Stop-ExtWatcher {
    Stop-Process -Name "ExtWatcher Service"
    Stop-Service -Name "ExtWatcherService"
}