param(
    [string]$value
)

Write-Host "You entered $value"

function SecureStringToPlainText([SecureString]$SecurePassword){
    $BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($SecurePassword)
    return [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)
}

$SecurePassword = Read-Host -Prompt "Enter password" -AsSecureString
$PlainPassword = SecureStringToPlainText($SecurePassword)

Write-Host "Pass: $($PlainPassword)"