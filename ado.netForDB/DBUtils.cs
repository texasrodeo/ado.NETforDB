using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ado.netForDB
{
    public class DBUtils
    {

        private string connectionString;
        

        public DBUtils(string connectionString)
        {
            this.connectionString = connectionString;
           
        }

        private SqlConnection GetConnection()
        {
           
            return new SqlConnection(this.connectionString);          
        }

        public void GetInfo()
        {
            using(SqlConnection connection = GetConnection())
            {
                connection.Open();
                Console.WriteLine("Свойства подключения:");
                Console.WriteLine("\tСтрока подключения: {0}", connection.ConnectionString);
                Console.WriteLine("\tБаза данных: {0}", connection.Database);
                Console.WriteLine("\tСервер: {0}", connection.DataSource);
                Console.WriteLine("\tВерсия сервера: {0}", connection.ServerVersion);
                Console.WriteLine("\tСостояние: {0}", connection.State);
                Console.WriteLine("\tWorkstationld: {0}", connection.WorkstationId);
            }
        }

        public int ExecuteNonQuery(string query)
        {
            int number = 0;
            using(SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    number = command.ExecuteNonQuery();
                    Console.WriteLine("Успешное выполнение запроса");
                }
                catch(SqlException e)
                {
                    Console.WriteLine("Ошибка");
                    Console.WriteLine(e.Number);
                    Console.WriteLine(e.Message);
                }
                
                
            
            return number;
            }
        }

        private static string GetTableName(string query)        
        {
            string newQuery = query.ToUpper();
            string[] split = query.Split();

            return split[Array.IndexOf(split, "FROM") + 1];
        }

        //private static List<string> GetColumns(string query)
        //{
        //    string newQuery = query.ToUpper();
        //    string[] split = query.Split();

        //    Regex regex = new Regex(@"туп(\w*)", RegexOptions.IgnoreCase);
        //}

        public void ExecuteSelect(string query)
        {
            using(SqlConnection connection = GetConnection()) 
            {
                try
                {
                   
                    connection.Open();
                   
                    DataTable table = connection.GetSchema("Columns", new string[] { null, null, GetTableName(query) });
                   
                    try
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                Console.Write(table.Rows[i]["COLUMN_NAME"] + "       ");
                            }
                            Console.WriteLine();
                            while (reader.Read())
                            {
                                for (int i = 0; i < table.Rows.Count; i++)
                                {
                                    Console.Write( reader.GetValue(i) + "       ");
                                }
                                Console.WriteLine();
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Ошибка");
                        Console.WriteLine(e.Number);
                        Console.WriteLine(e.Message);
                    }

                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                }
                
                
            }
           
        }

    }
}
