using System;
using System.Linq;
using Northwind.Data;

namespace Northwind.Client
{
    class Program
    {
        static void Main()
        {
            // Task 2
            var customer = new Customer
            {
                CustomerID = "Evala",
                Address = "Ivan Vazov",
                City = "Sofia",
                CompanyName = "SAP",
                ContactName = "Pesho",
                ContactTitle = "ADSD",
                Country = "Bulgaria"
            };
            // Uncomment the lines below
            //DataAccess.InsertCustomer(customer);
            //DataAccess.ModifyCustomer(customer, "HP");
            //DataAccess.DeleteCustomer(customer);

            // Task 3
            var customerOrders = DataAccess.GetCustomerOrders(1997, "Canada");
            Console.WriteLine(string.Join("\n\n", customerOrders));
            Console.WriteLine();

            // Task 4
            var customerOrdersSQL = DataAccess.GetCustomerOrdersWithNativeSQL(1997, "Canada");
            Console.WriteLine(string.Join("\n", customerOrdersSQL));
            Console.WriteLine();

            // Task 5
            var salesBySpec = DataAccess.GetSalesByPeriodAndRegion(
                "RJ", new DateTime(1996, 8, 22), new DateTime(1996, 9, 19));
            foreach (var sale in salesBySpec)
            {
                Console.WriteLine("Ship Region: {0} -> Order Date: {1} -> Required Date: {2}",
                    sale.ShipRegion, sale.OrderDate, sale.RequiredDate);
            }
            Console.WriteLine();

            // Task 9 Inseting new order
            customer = new Customer
            {
                CustomerID = "VETo4",
                Address = "Ivan Vazov",
                City = "Sofia",
                CompanyName = "SAP",
                ContactName = "Pesho",
                ContactTitle = "ADSD",
                Country = "Bulgaria"
            };
            var order = new Order
            {
                CustomerID = "VETo4",
                Customer = customer,
                EmployeeID = 6
            };
            //DataAccess.AddNewOrder(order);

            // Task 6
            //DataAccess.CloneDatabase();

            // Task 10
            using (var db = new NorthwindEntities())
            {

                //// Uncomment to create the procedure -> update the .edx file and the execute the method
                //db.Database.ExecuteSqlCommand(
                //    "CREATE PROC dbo.usp_FindTotalIncomes(" +
                //        "@companyName nvarchar(50), @startDate date, @endDate date) " +
                //    "AS " +
                //    "BEGIN " +
                //        "SELECT SUM(p.UnitPrice * p.UnitsOnOrder) AS [Sum] " +
                //        "FROM Products p " +
                //        "JOIN Suppliers s " +
                //            "ON p.SupplierID = s.SupplierID " +
                //        "JOIN [Order Details] od " +
                //            "ON od.ProductID = p.ProductID " +
                //        "JOIN Orders o " +
                //            "ON o.OrderID = od.OrderID " +
                //        "WHERE s.CompanyName = @companyName AND o.OrderDate BETWEEN @startDate AND @endDate " +
                //    "END");

                var supplier = "Lyngbysild";
                var startDate = new DateTime(1997, 12, 12);
                var endDate = new DateTime(1998, 12, 12);
                var result = db.usp_FindTotalIncomes(
                    supplier, startDate, endDate).FirstOrDefault();
                Console.WriteLine("Total incomes for supplier {0} is {1}", supplier, result);
                Console.WriteLine();
            }
        }
    }
}