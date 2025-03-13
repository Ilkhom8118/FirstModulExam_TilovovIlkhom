using BII.Services;
using Bll.Service;
using Dal;
using Dal.Migrations;
using Microsoft.Extensions.DependencyInjection;
using TgBot_UserInfo;

namespace G10_4Modul_TilovovIlkhom
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IUserInfoService, UserInfoService>();
            serviceCollection.AddSingleton<MainContext>();
            serviceCollection.AddSingleton<TelegramBotListener>();


            var serviceProvider = serviceCollection.BuildServiceProvider();

            var botListenerService = serviceProvider.GetRequiredService<TelegramBotListener>();
            await botListenerService.StartBot();

            Console.ReadKey();
        }
    }
}
// ==================================================================================================================

//CREATE TABLE Customers(
//	CustomerId INT PRIMARY KEY IDENTITY(1,1),
//    Name NVARCHAR(100),
//    Email NVARCHAR(100)
//);

//CREATE TABLE Orders(
//	OrderId INT PRIMARY KEY IDENTITY(1,1),
//    CustomerId INT NOT NULL,
//    OrderDate DATETIME DEFAULT GETDATE(),

//    CONSTRAINT FK_Order_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
//);

//CREATE TABLE OrderItems(
//	OrderItemId INT PRIMARY KEY IDENTITY(1,1),
//    OrderId INT NOT NULL,
//    ProductId INT NOT NULL,
//    Quantity INT,
//    Price DECIMAL(10,2),
//    CONSTRAINT FK_OrderItem_OrderId FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
//    CONSTRAINT FK_OrderItem_ProductId FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
//);

//CREATE TABLE Products(
//	ProductId INT PRIMARY KEY IDENTITY(1,1),
//    Name NVARCHAR(100),
//    Price DECIMAL(10,2)
//);


// ================================== 1 SAVOL ==================================
//SELECT* FROM Orders
//WHERE OrderDate >= DATEADD(DAY, -30, GETDATE());

// ================================== 2 SAVOL ==================================

//SELECT TOP 1 c.CustomerID, c.Name, COUNT(o.OrderID) AS TotalOrders
//FROM Customers c
//JOIN Orders o ON c.CustomerID = o.CustomerID
//GROUP BY c.CustomerID, c.Name
//ORDER BY TotalOrders DESC;

// ================================== 3 SAVOL ==================================

//SELECT* FROM Products
//WHERE ProductID NOT IN (SELECT DISTINCT ProductID FROM OrderItems);

// ================================== 4  SAVOL ==================================

//SELECT p.ProductID, p.Name, SUM(oi.Quantity * oi.Price) AS TotalRevenue
//FROM Products p
//JOIN OrderItems oi ON p.ProductID = oi.ProductID
//GROUP BY p.ProductID, p.Name;

// ================================== 5  SAVOL ==================================

//SELECT MONTH(OrderDate) AS OrderMonth, COUNT(OrderID) AS TotalOrders
//FROM Orders
//WHERE YEAR(OrderDate) = 2024
//GROUP BY MONTH(OrderDate)
//ORDER BY OrderMonth;