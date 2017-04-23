using System;
using System.Data;
using System.Data.OleDb;

class Excel
{
    static void Main()
    {
        string fileName = "../../scoreboard.xlsx";
        DataSet sheet = new DataSet();
        OleDbConnectionStringBuilder conString = new OleDbConnectionStringBuilder();
        conString.Provider = "Microsoft.ACE.OLEDB.12.0";
        conString.DataSource = fileName;
        conString.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES");

        using (var dbCon = new OleDbConnection(conString.ConnectionString))
        {
            dbCon.Open();
            string command = "SELECT * FROM [Sheet1$]";
            using (var adapter = new OleDbDataAdapter(command, dbCon))
            {
                adapter.Fill(sheet);
            }

            string name = "Joro the rabbit";
            double score = 434.34;
            AddToExcel(name, score, dbCon);
        }

        DataTable table = sheet.Tables[0];
        PrintTable(table);
    }

    static void PrintTable(DataTable table)
    {
        foreach (DataRow row in table.Rows)
        {
            foreach (DataColumn column in table.Columns)
            {
                Console.Write("{0}|", row[column]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static void AddToExcel(string name, double score, OleDbConnection dbCon)
    {
        var command = new OleDbCommand(
            "INSERT INTO [Sheet1$] (Name, Score) VALUES (@name, @score)", dbCon);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@score", score);
        command.ExecuteNonQuery();
        Console.WriteLine("Inserted: Name: {0}, Score: {1}\n", name, score);
    }
}