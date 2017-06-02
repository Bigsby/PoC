param(
    [switch]$usage,
    [switch]$h,
    [switch]$help,
    [string]$makeCertPath = "C:\Program Files (x86)\Windows Kits\10\bin\x86\makecert.exe",
    [string]$pvk2pfxPath = "C:\Program Files (x86)\Windows Kits\10\bin\x86\pvk2pfx.exe",
    [string]$certUtilPath = "C:\Windows\System32\certutil.exe",
    [string]$cac,
    [string]$cap,
    [string]$caf = "localDevCA",
    [string]$can = "CN=Local Development CA",
    [string]$lhf = "localhostDev",
    [string]$lhn = "CN=localhost",
    [string]$lp = "123456",
    [string]$cf = "testClient",
    [string]$cn = "Test Client",
    [string]$ud = "+",
    [int]$port = "9000",
    [string]$aid = "3bc01322-0976-4d7d-a505-77a523f6b3a3"
)

if ($usage -or $help -or $h){
    Write-Host "This script allows the creation and import of certificates, adding URL resevation and adding SSL certificate to the URL reservation. The usage is as follows:"
    Write-Host "Terminology:"
    Write-Host " - CA = Certificate Authority"
    Write-Host
    Write-Host "USAGE"
    Write-Host "./$($MyInvocation.MyCommand.Name) [[-cac <CACertificateCerPath> -cap <CACeritificatePvkPath>] | [[-caf <NewCACerPath>] [-can `"<NewCaX509Name>`"]]] [-lhf <SslCerPath>] [-lhn <SslX509Name>] [-lp <SslPfxPassword>] [-ud <urlDomain>] [-port <listeningPort>]"
    Write-Host
    Write-Host "    -cac        The path to an existing CA certificate to use to create the SSL Certificate. Requires -cap to be set."
    Write-Host "                    eg: -cac caCertificate.cer"
    Write-Host "    -cap        The path to the existing CA certificate private key. Only used if -cac is set."
    Write-Host "                    eg: -cac caCertificate.pvk"
    Write-Host
    Write-Host "    -caf        When a -cac is not set, the path of the new CA certificate. Only used if -cac is not set. Default is `"localDevCA`""
    Write-Host "                    eg: -caf newCA"
    Write-Host "                        Will generate newCA.cer and newCA.pvk (if not existing)."
    Write-Host "    -can        When a -cac is not set, the X509 name of the new CA certificate. Only used if -cac is not set. Default is `"CN=Local Development CA`""
    Write-Host "                    eg: -can `"O=The Organization,OU=The Unit,CN=My CA`""
    Write-Host
    Write-Host "    -lhf        The path to the new SSL certificate. Default is `"localhostDev`""
    Write-Host "                    eg: -caf localCert"
    Write-Host "                        Will generate localCert.cer, localCert.pvk (if not existing) and localCert.pfx."
    Write-Host "    -lhn        The X509 name of the new SSL certificate. Default is `"CN=localhost`""
    Write-Host "                    CN value must match domain the certificate will be used on, e.g., localhost (for local access only), machine name or IP for remote access. "
    Write-Host "                    eg: -can `"O=The Organization,OU=The Unit,CN=sitedomain`""
    Write-Host "    -lp         The password for the SSL Certificate. Default is `"123456`""
    Write-Host "                    eg: -lp p@`$`$w0rd"
    Write-Host
    Write-Host "    -ud         The URL domain to reserve in the HTTP namespace. Defautl is `"+`""
    Write-Host "                    eg: -ud local.site"
    Write-Host "    -port       The port to reserve in the HTTP namespace. Defautl is `"9000`""
    Write-Host "                    eg: -port 9443"
    Write-Host 
    Write-Host "REMARKS"
    Write-Host "    Firefox needs CA certificate to be installed manually, trust the SSL certificate, by going to:"
    Write-Host "            Options > Advanced > Certificates > View Certificates (button)" 
    Write-Host "            Authorities Tab > Import... (button)"
    Write-Host "            Select CA .cer file"
    Write-Host "            enable `"Trust this CA to identify websites`" and OK (button)"
    Write-Host 
    Write-Host "    Chrome does not acc"
    exit
}

function CheckPath($path, $errorMessage){
    Write-Host "Testing $path"
    if (-not (Test-Path $path)){
        Write-Warning $errorMessage
        exit
    }
}

CheckPath -path $makeCertPath -errorMessage "makecert.exe not found in `"$makeCertPath`". Please provide -makeCertPath parameter."
CheckPath -path $pvk2pfxPath -errorMessage "pvk2pfx.exe not found in `"$pvk2pfxPath`". Please provide -pvk2pfxPath parameter."
CheckPath -path $certUtilPath -errorMessage "certUtil.exe not found in `"$certUtilPath`". Please provide -certUtilPath parameter."

function InvokeCommand($command) {
    if ($command[0] -eq '"') { return Invoke-Expression "& $command" }
    else { return Invoke-Expression $command }
}

function CreateCertificate($name, $file, $selfSigned, $cy, $ic, $iv, $sky, $eku) {
    $command = "`"$makeCertPath`"" 
    if ($selfSigned) { $command += " -r" }
    $command += " -n `"$name`""
    $command += " -sv $file.pvk"
    $command += " -pe"
    if ($cy) { $command += " -cy $cy" }
    if ($ic) { $command += " -ic $ic" }
    if ($iv) { $command += " -iv $iv" }
    if ($sky) { $command += " -sky $sky" }
    if ($eky) { $command += " -eky $eku" }

    $command += " $file.cer"
    InvokeCommand($command)
}

function GetCertificateHash($certPath) {
    $cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2
    $cert.Import($certPath)
    return $cert.Thumbprint
}

function ImportCertificate($cert, $store){
    & $certUtilPath -addstore $store $cert
}

function UrlExists([string] $url) {
    return netsh http show urlacl url=$url | Where-Object { $_ -match [regex]::Escape($url)}
}

function RemoveUrl([string] $url) {
    if (UrlExists($url)) {
        netsh http delete urlacl url=$url
    }
}

function AddUrl([string] $url) {
    if (-not (UrlExists($url))) { 
        netsh http add urlacl url=$url user=Everyone
    }
}

function ShowUrl([string] $url) {
    netsh http show urlacl url=$url
}

function AddSSLCert($appId, $port, $sslCertificateHash) {
    if (netsh http show sslcert ipport=0.0.0.0:$port | Where-Object { $_ -match [regex]::Escape("0.0.0.0:$port")}) {
        Write-Host "Removing SSL Certificate found for 0.0.0.0:$port"
        netsh http delete sslcert ipport=0.0.0.0:$port
    }
    & netsh http add sslcert ipport=0.0.0.0:$port certhash=$sslCertificateHash "appid={$appId}"
}

$caCert = $cac
if (-not ($cac)){
    Write-Host "Creating CA certificate..."
    CreateCertificate $can  $caf $true "authority"
    $caCert = $caf + ".cer"
    Write-Host "---------------///---------------"
} else {
    if (-not ($cap)){
        Write-Warning "When Certificate Authority Certificate is provided, its private key (-cap) must also be provided."
        exit
    } else {
        CheckPath -path $cac -errorMessage "Certificate Authority Certificate ($cac) not found."
        CheckPath -path $cac -errorMessage "Certificate Authority Private Key ($cap) not found."
    }
}

Write-Host "Creating localhost certificate..."
CreateCertificate $lhn $lhf $false "" "$caCert" "$caf.pvk" "exchange" "1.3.6.1.5.5.7.3.1"
Write-Host "---------------///---------------"

Write-Host "Creating localhost PFX"
InvokeCommand "`"$pvk2pfxPath`" -f -pvk $lhf.pvk -spc $lhf.cer -pfx $lhf.pfx -po $lp"
Write-Host "---------------///---------------"

Write-Host "Removing existing cert for $lhn"
InvokeCommand "`"$certUtilPath`" -delstore my `"$lhn`""
Write-Host "---------------///---------------"

Write-Host "Importing PFX for $lhn"
InvokeCommand "`"$certUtilPath`" -p $lp -importpfx $lhf.pfx"
Write-Host "---------------///---------------"

Write-Host "Adding CA Ceriticate to Trusted Root CA store"
ImportCertificate "$caCert" "Root"
Write-Host "---------------///---------------"

$certHash = GetCertificateHash((Get-Location).Path + "\$lhf.cer")
$fullUrl = "https://${ud}:$port/"

Write-Host "Removing existing URL Reservation..."
RemoveUrl($fullUrl)
Write-Host "---------------///---------------"

Write-Host "Adding existing URL Reservation..."
AddUrl($fullUrl)
Write-Host "---------------///---------------"
Write-Host $certHash
Write-Host "Adding SSL Ceritificate to URL Reservation..."
AddSSLCert $aid $port $certHash
Write-Host "---------------///---------------"
Write-Host "Done!"
Write-Host "If no errors occured, you can use `"$fullUrl`" as a self hosted web application."

# Firefox:
# > Options > Advanced > Certificates > View Certificates > Authorities > Import... > select "CA.cer" > enable "Trust this CA to identify websites"
# https://localhost:9000/api/deviceregistration?identity=asdfasdf
# https://technet.microsoft.com/itpro/powershell/windows/pkiclient/new-selfsignedcertificate