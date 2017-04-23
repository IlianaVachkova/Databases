using System;
using System.Data.SqlClient;
using _1.CategoriesCount;

class Program
{
    static void Main()
    {
        SqlConnection dbCon = new SqlConnection(Settings.Default.DBConnectionString);
        dbCon.Open();

        Console.WriteLine("Task 1 -------------------------");
        var rowsCountCommand = new SqlCommand(
            "SELECT COUNT(CategoryID) FROM Categories", dbCon);
        int rowsCount = (int)rowsCountCommand.ExecuteScalar();
        Console.WriteLine("Total rows count is: {0}", rowsCount);
        Console.WriteLine();

        Console.WriteLine("Task 2 -------------------------");
        Console.WriteLine("Categories");
        var namesAndDescriptionCommand = new SqlCommand(
            "SELECT CategoryName, Description FROM Categories", dbCon);
        var reader = namesAndDescriptionCommand.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine("Name: {0} | Description: {1}", 
                reader["CategoryName"], reader["Description"]);
        }
        reader.Close();
        Console.WriteLine();

        Console.WriteLine("Task 3 -------------------------");
        var productsAndCategoriesCommand = new SqlCommand(
            "SELECT c.CategoryName, p.ProductName " +
            "FROM Categories c JOIN Products p " +
	            "ON c.CategoryID = p.CategoryID", dbCon);
        reader = productsAndCategoriesCommand.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine("Category: {0} | Product: {1}",
                reader["CategoryName"], reader["ProductName"]);
        }
        reader.Close();
        Console.WriteLine();

        Console.WriteLine("Task 4 -------------------------");
        var insertProductCommand = new SqlCommand(
            "INSERT INTO Products (ProductName, Discontinued) " +
            "VALUES (@productName, @discontinued)", dbCon);
        string productName = "Pepsi";
        int discontinued = 0;
        insertProductCommand.Parameters.AddWithValue("@productName", productName);
        insertProductCommand.Parameters.AddWithValue("@discontinued", discontinued);
        insertProductCommand.ExecuteNonQuery();
        SqlCommand cmdSelectIdentity = new SqlCommand("SELECT @@Identity", dbCon);
        int insertedProductId = (int)(decimal)cmdSelectIdentity.ExecuteScalar();
        Console.WriteLine("Product: {0} with ID: {1}, added into Products", 
            productName, insertedProductId);
        Console.WriteLine();

        dbCon.Close();
    }
}