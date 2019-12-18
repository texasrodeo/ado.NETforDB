using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ado.netForDB
{
    class Program
    {
        static void ShowMenu(DBUtils dBUtils)
        {
            Console.WriteLine("----------------");
            Console.WriteLine("Меню:");
            Console.WriteLine("1 - Выполнить SELECT запрос");
            Console.WriteLine("2 - Выполнить INSERT, DELETE, UPDATE запрос");
            Console.WriteLine("----------------");
            Console.WriteLine("Выберите операцию:");
            int operation = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите запрос:");
            string query = Console.ReadLine();
            switch (operation)
            {
                case 1:
                    dBUtils.ExecuteSelect(query);
                    break;
                case 2:
                    dBUtils.ExecuteNonQuery(query);
                    break;
                default:
                    Console.WriteLine("Неверно введена операция");
                    break;
            }
        }

        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            DBUtils dBUtils = new DBUtils(connectionString);

            dBUtils.GetInfo();
            Console.WriteLine();

            while (true)
            {
                ShowMenu(dBUtils);
            }                      
        }
    }
}
