using System;
using System.Data.SQLite;
using _10.SQLiteDB;

class SQLiteDB
{
    static void Main()
    {
        var connection = new SQLiteConnection(Settings.Default.DBConnectionString);
        connection.Open();
        using (connection)
        {
            var title = "Die Hard";
            var author = "Richi";
            ListBooksByTitle(connection, title);
            FindBookByTitle(connection, title);
            AddBookByTitleAndAuthor(connection, title, author, 5);
        }
    }

    static void FindBookByTitle(SQLiteConnection connection, string title)
    {
        var command = new SQLiteCommand("SELECT COUNT(Title) FROM Books WHERE Title = @title", connection);
        command.Parameters.AddWithValue("@title", title);
        var resultCount = command.ExecuteScalar();

        Console.WriteLine("The searched book {0} ocurred {1} times", title, resultCount);
    }

    static void ListBooksByTitle(SQLiteConnection connection, string title)
    {
        var command = new SQLiteCommand(
            "SELECT Author, Title FROM Books WHERE Title = @title", connection);
        command.Parameters.AddWithValue("@title", title);
        SQLiteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine("{0} - {1}",
                reader["Author"], reader["Title"]);
        }

        reader.Close();
    }

    static void AddBookByTitleAndAuthor(
        SQLiteConnection connection, string title, string author, int bookId)
    {
        var command = new SQLiteCommand(
            "INSERT INTO Books (BookId, Title, Author) VALUES (@bookId, @title, @author)", connection);
        command.Parameters.AddWithValue("@title", title);
        command.Parameters.AddWithValue("@author", author);
        command.Parameters.AddWithValue("@bookId", bookId);
        command.ExecuteNonQuery();

        Console.WriteLine("You inserted book {0}, with title {1}", title, author);
    }
}