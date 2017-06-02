param(
    [string]$value
)
Write-Host "In $($MyInvocation.MyCommand.Name)"

$somevar = "variable"
Write-Host (Get-Variable "somevar").Name

& ./test2 -value another

Write-Host "value: $value"