using System;
using _9.MySQLDB;
using MySql.Data.MySqlClient;

class MySQLDB
{
    static void Main()
    {
        var connection = new MySqlConnection(Settings.Default.DBConnectionString);
        connection.Open();
        using (connection)
        {
            var title = "Die Hard";
            var author = "Richi";
            ListBooksByTitle(connection, title);
            FindBookByTitle(connection, title);
            AddBookByTitleAndAuthor(connection, title, author);
        }        
    }

    static void FindBookByTitle(MySqlConnection connection, string title)
    {
        var command = new MySqlCommand("SELECT COUNT(Title) FROM Books WHERE Title = @title", connection);
        command.Parameters.AddWithValue("@title", title);
        var resultCount = command.ExecuteScalar();

        Console.WriteLine("The searched book {0} ocurred {1} times", title, resultCount);
    }

    static void ListBooksByTitle(MySqlConnection connection, string title)
    {
        var command = new MySqlCommand(
            "SELECT Author, Title FROM Books WHERE Title = @title", connection);
        command.Parameters.AddWithValue("@title", title);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine("{0} - {1}", 
                reader["Author"], reader["Title"]);
        }

        reader.Close();
    }

    static void AddBookByTitleAndAuthor(MySqlConnection connection, string title, string author)
    {
        var command = new MySqlCommand(
            "INSERT INTO Books (Title, Author) VALUES (@title, @author)", connection);
        command.Parameters.AddWithValue("@title", title);
        command.Parameters.AddWithValue("@author", author);
        command.ExecuteNonQuery();

        Console.WriteLine("You inserted book {0}, with title {1}", title, author);
    }
}