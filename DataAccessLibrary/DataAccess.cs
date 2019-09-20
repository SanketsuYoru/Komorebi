using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{

    public class Database{
        public string Token { set; get; }
        public string Folder { set; get; }
    }

    public static class FaTokenDataAccess
    {
        public static void InitializefaTokenDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=faToken.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                    "Tokens NVARCHAR(2048) NULL,"+ " Folders NVARCHAR(2048) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
                createTable.Dispose();
                db.Close();
            }
        }
        public static void AddData(Database Data_in)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=faToken.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @Token,@Folder);";
                insertCommand.Parameters.AddWithValue("@Token", Data_in.Token);
                insertCommand.Parameters.AddWithValue("@Folder", Data_in.Folder);
                insertCommand.ExecuteReader();
                insertCommand.Dispose();
                db.Close();
                db.Dispose();
            }

        }
        public static List<Database> GetData()
        {
            Database entrie = new Database();
            List<Database> entries = new List<Database>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=faToken.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Tokens,Folders from MyTable", db);

                SqliteDataReader query = selectCommand.ExecuteReader();
                //Read 方法将向前浏览返回的数据的行。 如果有剩下的行，它将返回 true，否则返回 false。
                while (query.Read())
                {
                    entrie.Token=(query.GetString(0));
                    entrie.Folder=(query.GetString(1));
                    entries.Add(entrie);
                    entrie = new Database();
                }
                db.Close();
                db.Dispose();
            }

            return entries;
        }

        public static void DeleteData(string @inputText)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=faToken.db"))
            {
                db.Open();
                SqliteCommand command = db.CreateCommand();
                command.CommandText = "DELETE from MyTable WHERE Folders =  @inputText";
                command.Parameters.Add(new SqliteParameter("@inputText", @inputText));
                command.ExecuteNonQuery();
                //Read 方法将向前浏览返回的数据的行。 如果有剩下的行，它将返回 true，否则返回 false。
                db.Close();
                db.Dispose();
            }
        }
    }


        public static class MruTokenDataAccess
        {
        public static void DeleteData(string @inputText)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=mruToken.db"))
            {
                db.Open();
                SqliteCommand command = db.CreateCommand();
                command.CommandText = "DELETE from MyTable WHERE Text_Entry =  @inputText";
                command.Parameters.Add(new SqliteParameter("@inputText", @inputText));
                command.ExecuteNonQuery();
                //Read 方法将向前浏览返回的数据的行。 如果有剩下的行，它将返回 true，否则返回 false。
                db.Close();
                db.Dispose();
            }
        }


        public static void InitializemruTokenDatabase()
            {
                using (SqliteConnection db =
                    new SqliteConnection("Filename=mruToken.db"))
                {
                    db.Open();

                    String tableCommand = "CREATE TABLE IF NOT " +
                        "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                        "Text_Entry NVARCHAR(2048) NULL)";

                    SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                    createTable.ExecuteReader();
                createTable.Dispose();
                    db.Close();
                db.Dispose();
            }
           
        }
            public static void AddData(string inputText)
            {
                using (SqliteConnection db =
                    new SqliteConnection("Filename=mruToken.db"))
                {
                    db.Open();

                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @Entry);";
                    insertCommand.Parameters.AddWithValue("@Entry", inputText);
                    insertCommand.ExecuteReader();
                insertCommand.Dispose();
                    db.Close();
                db.Dispose();
            }

            }
            public static List<String> GetData()
            {
                List<String> entries = new List<string>();

                using (SqliteConnection db =
                    new SqliteConnection("Filename=mruToken.db"))
                {
                    db.Open();

                    SqliteCommand selectCommand = new SqliteCommand
                        ("SELECT Text_Entry from MyTable", db);

                    SqliteDataReader query = selectCommand.ExecuteReader();
                    //Read 方法将向前浏览返回的数据的行。 如果有剩下的行，它将返回 true，否则返回 false。
                    while (query.Read())
                    {
                        entries.Add(query.GetString(0));
                    }
                db.Close();
                db.Dispose();
            }
                return entries;
            }
        }

    }


