using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.dip
{
    internal class Case3
    {
        // Абстракция для работы с БД
        public interface IDatabase
        {
            void Connect();
            void Disconnect();
            List<string> GetRecords();
        }

        // Абстракция для записи логов 
        public interface ILogWriter
        {
            void WriteLogEntry(string entry);
        }

        // Реализация базы данных MySQL
        public class MySQLDatabase : IDatabase
        {
            private const int ConnectionDelayMs = 3000; // Без магических цифр
            private readonly string _connectionString; // Чтобы не путать с тем что в методах
            // Поэтому везде добавил приставку _

            // Конструктор
            public MySQLDatabase(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void Connect()
            {
                Thread.Sleep(ConnectionDelayMs);
                Console.WriteLine($"Connection done {_connectionString}"); // Мне интерполяция не нравится
            }

            public void Disconnect()
            {
                Console.WriteLine($"Disconnected from {_connectionString}"); // Но в этом конкатенации нет
            }

            public List<string> GetRecords()
            {
                return new List<string> { "data1", "data2" };
            }
        }

        // Реализация логирования в файл
        public class FileLogWriter : ILogWriter
        {
            private readonly string _filePath;

            public FileLogWriter(string filePath = "log.txt")
            {
                _filePath = filePath;
            }

            public void WriteLogEntry(string entry)
            {
                File.WriteAllText(_filePath, entry);
            }
        }

        // Генератор отчётов, зависящий от абстракций
        public class ReportGenerator
        {
            private const int MaxReportItems = 100;
            private readonly IDatabase _database;
            private readonly ILogWriter _logWriter;
            private List<string> _reportData = new List<string>();

            public ReportGenerator(IDatabase database, ILogWriter logWriter)
            {
                _database = database ?? throw new ArgumentNullException(nameof(database));
                _logWriter = logWriter ?? throw new ArgumentNullException(nameof(logWriter));
            }

            public void GenerateReport()
            {
                _database.Connect();
                _reportData = _database.GetRecords();

                ProcessData();
                var formattedLines = FormatReport();
                SaveReport(string.Join(';', formattedLines));
            }

            private void ProcessData()
            {
                if (_reportData.Count > MaxReportItems)
                {
                    _reportData = _reportData.GetRange(0, MaxReportItems);
                }
            }

            private List<string> FormatReport()
            {
                var result = new List<string>();
                foreach (var item in _reportData)
                {
                    var formatted = $"Item: {item.ToUpper()}";
                    result.Add(formatted);
                    Console.WriteLine(formatted);
                }
                return result;
            }

            private void SaveReport(string formattedText)
            {
                _logWriter.WriteLogEntry(formattedText);
            }
        }
    }
}
