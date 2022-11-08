# exchange lemon
sample project for crypto exchange

## requirement
- use mssql server
- dependency to rabbitmq

### build
- main project  
/exchangelemondraft/ExchangeLemon/ExchangeLemonCore 
- main API   
 /exchangelemondraft/ExchangeLemon/ExchangeLemonCore/Controllers/ControllersApi/OrderItemMainController.cs

### build ef

````bash
dotnet ef database update -c LoggingExtContext
````

````bash
dotnet ef database update -c SourceContext
````

````bash
dotnet ef database update -c ApplicationContext
````




