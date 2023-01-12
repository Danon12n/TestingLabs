cd TestingLabs
dotnet test 
cd ..
call allure-2.20.1\bin\allure.bat serve TestingLabs\pet_shop\bin\Debug\netcoreapp3.1\allure-results\