#git clone / set credentials
git clone https://github.com/sabsfilho/InsuranceAdvisor
git config --global user.name "type name"
git config --global user.email "type email"
#create project structure
#https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new
dotnet new gitignore
mkdir InsuranceAdvisorLib
cd InsuranceAdvisorLib
dotnet new classlib -f net8.0
cd ..
mkdir InsuranceAdvisorApp
cd InsuranceAdvisorApp
dotnet new web -f net8.0
dotnet add reference ../InsuranceAdvisorLib/InsuranceAdvisorLib.csproj
cd ..
dotnet new sln --name InsuranceAdvisor
dotnet sln add InsuranceAdvisorLib
dotnet sln add InsuranceAdvisorApp
dotnet run --project InsuranceAdvisorApp
#simple webapi mounted so far...
