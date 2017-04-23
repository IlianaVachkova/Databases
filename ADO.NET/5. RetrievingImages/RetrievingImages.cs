using System;
using System.Data.SqlClient;
using System.IO;
using _1.CategoriesCount;

static class RetrievingImages
{
    // Когато се работи с картинки, има блок, който се нарича metafilepict. 
    // Едва ли ще откриете картинка, ако го прочетете. 
    // Стартовата позиция на картинката се изчислява след като се изпусне този блок.
    // Metafilepict стартова позиция = дължината на package header-а + дължината на ole header-а + 8 празни байта + 4 байта, 
    // указващи дължината на данните + дължината на байтовете + 45 (дължината на metafilepict header-а). 
    // При картинките в Northwind това число е точно 78.

    private const int OleMetafilePictStartPosition = 78;

    static void Main()
    {
        SqlConnection dbCon = new SqlConnection(Settings.Default.DBConnectionString);
        dbCon.Open();

        using (dbCon)
        {
            var command = new SqlCommand(
                "SELECT CategoryName, Picture FROM Categories", dbCon);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                byte[] image = (byte[])reader["Picture"];
                string category = (string)reader["CategoryName"];
                string destFile = "../../images/" + category.EscapeDash() + ".jpg";
                WriteBinaryFile(destFile, image);
            }
        }
    }

    static void WriteBinaryFile(string fileName, byte[] fileContents)
    {
        FileStream stream = File.OpenWrite(fileName);
        stream.Write(fileContents, OleMetafilePictStartPosition,
            fileContents.Length - OleMetafilePictStartPosition);
        stream.Close();
    }

    static string EscapeDash(this string fileName)
    {
        return fileName.Replace("/", " ");
    }
}