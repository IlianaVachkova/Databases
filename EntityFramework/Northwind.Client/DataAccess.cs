using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Northwind.Data;

namespace Northwind.Client
{
    public class DataAccess
    {
        public static void InsertCustomer(Customer customer)
        {
            using (var db = new NorthwindEntities())
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }

        public static void DeleteCustomer(Customer customer)
        {
            using (var db = new NorthwindEntities())
            {
                db.Customers.Attach(customer);
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
        }

        public static void ModifyCustomer(Customer customer, string companyName)
        {
            using (var db = new NorthwindEntities())
            {
                db.Customers.Attach(customer);
                customer.CompanyName = companyName;
                db.SaveChanges();
            }
        }

        public static Customer GetCustomer(string id)
        {
            using (var db = new NorthwindEntities())
            {
                var result = db.Customers.Find(id);
                return result;
            }
        }

        public static IEnumerable<Customer> GetCustomerOrders(int year, string country)
        {
            using (var db = new NorthwindEntities())
            {
                var orders = db.Orders
                    .Where(x => x.OrderDate.Value.Year == year)
                    .Where(x => x.ShipCountry == country)
                    .Select(x => new { x.CustomerID })
                    .Distinct();

                var result = new List<Customer>();
                foreach (var order in orders)
                {
                    var customer = GetCustomer(order.CustomerID);
                    result.Add(customer);
                }

                return result;
            }
        }

        public static IEnumerable<string> GetCustomerOrdersWithNativeSQL(int year, string country)
        {
            var db = new NorthwindEntities();
            string nativeSqlQuery =
                "SELECT CustomerID + ' ' + ContactName + ' ' + CompanyName + ' ' + City " +
                "FROM dbo.Customers " +
                "WHERE CustomerId IN " +
                    "(SELECT CustomerId FROM dbo.Orders " +
                        "WHERE YEAR(OrderDate) = {0} AND ShipCountry = {1})";
            object[] parameters = { year, country };
            var customers = db.Database.SqlQuery<string>(
                nativeSqlQuery, parameters);
            return customers;
        }

        public static IEnumerable<Order> GetSalesByPeriodAndRegion(
            string region, DateTime startDate, DateTime endDate)
        {
            var db = new NorthwindEntities();
            var orders = db.Orders
                .Where(x =>
                    x.ShipRegion == region &&
                    x.ShippedDate >= startDate &&
                    x.ShippedDate <= endDate);

            return orders;
        }

        // Task 6
        public static void CloneDatabase()
        {
            IObjectContextAdapter context = new NorthwindEntities();
            string cloneNorthwind = context.ObjectContext.CreateDatabaseScript();

            string createNorthwindCloneDB = 
                "CREATE DATABASE NorthwindTwin ON PRIMARY " +
                    "(NAME = NorthwindTwin, " +
                    "FILENAME = 'E:\\NorthwindTwin.mdf', " +
                    "SIZE = 5MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                    "LOG ON (NAME = NorthwindTwinLog, " +
                    "FILENAME = 'E:\\NorthwindTwin.ldf', " +
                    "SIZE = 1MB, " +
                    "MAXSIZE = 5MB, " +
                    "FILEGROWTH = 10%)";

            SqlConnection dbConForCreatingDB = new SqlConnection(
                "Server=LOCALHOST; " +
                "Database=master; " +
                "Integrated Security=true");

            dbConForCreatingDB.Open();

            using (dbConForCreatingDB)
            {
                SqlCommand createDB = new SqlCommand(createNorthwindCloneDB, dbConForCreatingDB);
                createDB.ExecuteNonQuery();
            }

            SqlConnection dbConForCloning = new SqlConnection(
                "Server=LOCALHOST; " +
                "Database=NorthwindTwin; " +
                "Integrated Security=true");

            dbConForCloning.Open();

            using (dbConForCloning)
            {
                SqlCommand cloneDB = new SqlCommand(cloneNorthwind, dbConForCloning);
                cloneDB.ExecuteNonQuery();
            }
        }

        // Task 9
        public static void AddNewOrder(Order order)
        {
            var db = new NorthwindEntities();
            db.Database.Connection.Open();
            DbTransaction tran = db.Database.Connection.BeginTransaction();
            try
            {
                db.Orders.Add(order);
                tran.Commit();

                Console.WriteLine("New order placed by {0}",
                    order.Customer.ContactName);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                db.SaveChanges();
                db.Database.Connection.Close();
            }
        }
    }
}