using System;
using System.Data.SqlClient;
using _1.CategoriesCount;

class EscapingSymbols
{
    static void Main()
    {
        Console.Write("Enter string to search for: ");
        string match = Console.ReadLine();

        SqlConnection dbCon = new SqlConnection(Settings.Default.DBConnectionString);
        dbCon.Open();

        using (dbCon)
        {
            var command = new SqlCommand(
                "SELECT ProductName FROM Products WHERE CHARINDEX(@match, ProductName) > 0", dbCon);
            command.Parameters.AddWithValue("@match", match);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("{0}", reader[0]);
            }
        }
    }
}