param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../../"

$dbMigratorFolder = Join-Path $slnFolder "src/Test.Core.DbMigrator"

Write-Host "********* BUILDING DbMigrator *********" -ForegroundColor Green
Set-Location $dbMigratorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t test/core-db-migrator:$version .




$webFolder = Join-Path $slnFolder "src/Test.Core.Web"

Write-Host "********* BUILDING Web Application *********" -ForegroundColor Green
Set-Location $webFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t test/core-web:$version .

$hostFolder = Join-Path $slnFolder "src/Test.Core.HttpApi.Host"
$identityServerAppFolder = Join-Path $slnFolder "src/Test.Core.IdentityServer"

Write-Host "********* BUILDING Api.Host Application *********" -ForegroundColor Green
Set-Location $hostFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t test/core-api:$version .

Write-Host "********* BUILDING IdentityServer Application *********" -ForegroundColor Green
Set-Location $identityServerAppFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t test/core-identityserver:$version .


### ALL COMPLETED
Write-Host "COMPLETED" -ForegroundColor Green
Set-Location $currentFolder