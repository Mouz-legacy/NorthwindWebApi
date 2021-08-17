using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Northwind.ReportingServices.SqlService.ProductReports
{
    public class ProductReportService
    {
        private string connectionString = "Server=DESKTOP-F1JS2EK;Database=master;Trusted_Connection=True;";

        public async Task GetNorthwindProducts()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Подключение открыто");
                // Вывод информации о подключении
                Console.WriteLine("Свойства подключения:");
                Console.WriteLine($"\tСтрока подключения: {connection.ConnectionString}");
                Console.WriteLine($"\tБаза данных: {connection.Database}");
                Console.WriteLine($"\tСервер: {connection.DataSource}");
                Console.WriteLine($"\tВерсия сервера: {connection.ServerVersion}");
                Console.WriteLine($"\tСостояние: {connection.State}");
                Console.WriteLine($"\tWorkstationld: {connection.WorkstationId}");
            }
            Console.WriteLine("Подключение закрыто...");
            Console.WriteLine("Программа завершила работу.");
            Console.Read();
            //var service = new Northwind.ReportingServices.SqlService.ProductReports.ProductReportService();
            //await service.GetNorthwindProducts();
        }
    }
}
