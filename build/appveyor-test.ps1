dotnet test -c $env:CONFIGURATION --logger:trx --no-build .\tests\Swashbuckle.Examples.Auto.Tests\Swashbuckle.Examples.Auto.Tests.csproj

$wc = New-Object 'System.Net.WebClient'
$results = Resolve-Path .\tests\Swashbuckle.Examples.Auto.Tests\TestResults\*.trx
$result = $results[0]
$wc.UploadFile("https://ci.appveyor.com/api/testresults/mstest/$($env:APPVEYOR_JOB_ID)", ($result))
Write-Host 'Uploaded test result file' $result