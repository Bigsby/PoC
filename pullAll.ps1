$baseFolder = (Get-Location).Path
$command = "git pull"
Get-ChildItem -dir | ForEach-Object {
    Set-Location -Path $_.FullName
    Write-Host (Get-Location).Path
    Invoke-Expression -Command $command
    Write-Host
}

Set-Location $baseFolder