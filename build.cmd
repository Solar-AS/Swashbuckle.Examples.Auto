@SET config=Release

dotnet restore .\Swashbuckle.Examples.Auto.sln -v q

dotnet build .\Swashbuckle.Examples.Auto.sln -c %config% -v q

dotnet test .\tests\Swashbuckle.Examples.Auto.Tests\Swashbuckle.Examples.Auto.Tests.csproj -c %config% -v q

dotnet pack .\src\Swashbuckle.Examples.Auto\Swashbuckle.Examples.Auto.csproj -c %config% --no-build -v m

@ECHO.
@ECHO ----------------------------------------------------------------------------------------
@ECHO   SUPER-GREEN. Package available in .\src\Swashbuckle.Examples.Auto\bin\%config%\   
@ECHO ----------------------------------------------------------------------------------------
@ECHO.