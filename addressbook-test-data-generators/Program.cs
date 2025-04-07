using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using addressbook_tests;

namespace addressbook_test_data_generators
{
    internal class Program
    {
        /// <summary>
        /// После добавления Excel
        /// </summary>
        static void Main(string[] args)
        {
            // Путь H:\Repos\training_v3\addressbook-test-data-generators\bin\Debug
            // addressbook-test-data-generators.exe 2 groups.xml xml
            // addressbook-test-data-generators.exe 2 groups.json json
            // addressbook-test-data-generators.exe 2 groups.xlsx excel
            int count = Convert.ToInt32(args[0]);
            string filename = args[1];
            string format = args[2];
            
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(100),
                    Footer = TestBase.GenerateRandomString(100)
                });
            }
            if (format == "excel")
            {
                writeGroupsToExcelFile(groups, filename);
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    writeGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(groups, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format " + format);
                }

                writer.Close();
            }
            
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
        static void writeGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            // Запуск Excel через COM интерфейс
            Excel.Application app = new Excel.Application();
            // Открывает Excel
            app.Visible = true;
            // При создании нового докумнта, автоматически создается 1 стр, ее можно получить как ActiveSheet
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            // На страницу вписывается текст
            //sheet.Cells[1, 1] = "test";
            // Вписывание данных
            int row = 1;
            foreach (GroupData group in groups) 
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            // Удаление файла
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            // Сохранение результата
            wb.SaveAs(fullPath);
            // Закрытие приложения
            wb.Close();
            app.Visible = false;
            app.Quit();
        }
        /// <summary>
        /// До добавления формата Excel
        /// </summary>
        //static void Main(string[] args)
        //{
        //    // Путь H:\Repos\training_v3\addressbook-test-data-generators\bin\Debug
        //    // addressbook-test-data-generators.exe 2 groups.xml xml
        //    // addressbook-test-data-generators.exe 2 groups.json json
        //    // addressbook-test-data-generators.exe 2 groups.xml xml
        //    int count = Convert.ToInt32(args[0]);
        //    string format = args[2];
        //    StreamWriter writer = new StreamWriter(args[1]);
        //    List<GroupData> groups = new List<GroupData>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        groups.Add(new GroupData(TestBase.GenerateRandomString(10))
        //        {
        //            Header = TestBase.GenerateRandomString(100),
        //            Footer = TestBase.GenerateRandomString(100)
        //        });
        //    }
        //    if (format == "csv")
        //    {
        //        writeGroupsToCsvFile(groups, writer);
        //    }
        //    else if (format == "xml")
        //    {
        //        writeGroupsToXmlFile(groups, writer);
        //    }
        //    else if (format == "json")
        //    {
        //        writeGroupsToJsonFile(groups, writer);
        //    }
        //    else if (format == "excel")
        //    {
        //        writeGroupsToExcelFile(groups, writer);
        //    }
        //    else
        //    {
        //        System.Console.Out.Write("Unrecognized format " + format);
        //    }

        //    writer.Close();
        //}
    }
}
