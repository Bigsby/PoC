$databasefile = $PSScriptRoot + "\database.db"
$sqlFile = $PSScriptRoot + "\build_db.sql"

Write-Host "Removing $databasefile"
Remove-Item $databasefile

Write-Host "Running $sqlFile"
Get-Content $sqlFile -raw | sqlite3 $databasefile

Write-Host "Tebles:"
sqlite3 -header -column $databasefile ".tables"
Write-Host "Lint Errors:"
sqlite3 -header -column $databasefile ".lint fkey-indexes"
sqlite3 -header -column $databasefile ".show"