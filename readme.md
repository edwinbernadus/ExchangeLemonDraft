- menggunakan mssql server
- dependency to rabbitmq

main project -> /exchangelemondraft/ExchangeLemon/ExchangeLemonCore
main API -> /exchangelemondraft/ExchangeLemon/ExchangeLemonCore/Controllers/ControllersApi/OrderItemMainController.cs


dotnet ef database update -c LoggingExtContext
dotnet ef database update -c SourceContext
dotnet ef database update -c ApplicationContext
