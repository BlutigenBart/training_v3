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
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Reflection;

namespace addressbook_test_data_generators
// Путь H:\Repos\training_v3\addressbook-test-data-generators\bin\Debug
// addressbook-test-data-generators.exe group 2 groups.csv csv
// addressbook-test-data-generators.exe group 2 groups.xml xml
// addressbook-test-data-generators.exe group 2 groups.json json
// addressbook-test-data-generators.exe group 2 groups.xlsx excel

// addressbook-test-data-generators.exe contact 3 contacts.csv csv
// addressbook-test-data-generators.exe contact 3 contacts.xml xml
// addressbook-test-data-generators.exe contact 3 contacts.json json
// addressbook-test-data-generators.exe contact 3 contacts.xlsx excel
{
    internal class Program
    {
        /// <summary>
        /// Переделанный под группы и контакты
        /// </summary>
        static void Main(string[] args)
        {
            string dataType = args[0]; // тип данный group или contact
            int count = Convert.ToInt32(args[1]); // кол-во для генерации
            string filename = args[2]; // имя файла
            string format = args[3]; // Формат: xml, json, excel, csv

            // Списки для хранения
            List<GroupData> groups = new List<GroupData>();
            List<ContactData> contacts = new List<ContactData>();

            if (dataType == "group") // Генерация групп
            {

                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(100),
                        Footer = TestBase.GenerateRandomString(100)
                    });

                }
            }
            if (dataType == "contact") // Генерация контактов
            {
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData()
                    {
                        Firstname = TestBase.GenerateRandomString(20),
                        Middlename = TestBase.GenerateRandomString(20),
                        Lastname = TestBase.GenerateRandomString(20),
                        Nickname = TestBase.GenerateRandomString(20),
                        Title = TestBase.GenerateRandomString(20),
                        Company = TestBase.GenerateRandomString(20),
                        Address = TestBase.GenerateRandomString(20),
                        Home = TestBase.GenerateRandomString(20),
                        Mobile = TestBase.GenerateRandomString(20),
                        Work = TestBase.GenerateRandomString(20),
                        Fax = TestBase.GenerateRandomString(20),
                        Email = TestBase.GenerateRandomString(20),
                        Email2 = TestBase.GenerateRandomString(20),
                        Email3 = TestBase.GenerateRandomString(20),
                        Homepage = TestBase.GenerateRandomString(20)
                    });
                }
            }

            if (format == "excel")
            {
                if (dataType == "group") writeGroupsToExcelFile(groups, filename);
                else if (dataType == "contact")
                    writeContactsToExcelFile(contacts, filename);
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    if (dataType == "group") writeGroupsToCsvFile(groups, writer);
                    else if (dataType == "contact")
                        writeContactsToCsvFile(contacts, writer);
                }
                else if (format == "xml")
                {
                    if (dataType == "group") writeGroupsToXmlFile(groups, writer);
                    else if (dataType == "contact")
                        writeContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    if (dataType == "group") writeGroupsToJsonFile(groups, writer);
                    else if (dataType == "contact")
                        writeContactsToJsonFile(contacts, writer);
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

        static void writeContactsToCsvFile(List<ContactData> contscts, StreamWriter writer)
        {
            foreach (ContactData contact in contscts)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    contact.Firstname, contact.Middlename, contact.Lastname));
            }
        }

        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        static void writeContactsToExcelFile(List<ContactData> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            int row = 1;
            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.Firstname;
                sheet.Cells[row, 2] = contact.Middlename;
                sheet.Cells[row, 3] = contact.Lastname;
                sheet.Cells[row, 4] = contact.Nickname;
                sheet.Cells[row, 5] = contact.Title;
                sheet.Cells[row, 6] = contact.Company;
                sheet.Cells[row, 7] = contact.Address;
                sheet.Cells[row, 8] = contact.Home;
                sheet.Cells[row, 9] = contact.Mobile;
                sheet.Cells[row, 10] = contact.Work;
                sheet.Cells[row, 11] = contact.Fax;
                sheet.Cells[row, 12] = contact.Email;
                sheet.Cells[row, 13] = contact.Email2;
                sheet.Cells[row, 14] = contact.Email3;
                sheet.Cells[row, 15] = contact.Homepage;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }

            /// <summary>
            /// После добавления Excel
            /// </summary>
            //static void Main(string[] args)
            //{
            //    // Путь H:\Repos\training_v3\addressbook-test-data-generators\bin\Debug
            //    // addressbook-test-data-generators.exe 2 groups.xml xml
            //    // addressbook-test-data-generators.exe 2 groups.json json
            //    // addressbook-test-data-generators.exe 2 groups.xlsx excel
            //    int count = Convert.ToInt32(args[0]);
            //    string filename = args[1];
            //    string format = args[2];

            //    List<GroupData> groups = new List<GroupData>();
            //    for (int i = 0; i < count; i++)
            //    {
            //        groups.Add(new GroupData(TestBase.GenerateRandomString(10))
            //        {
            //            Header = TestBase.GenerateRandomString(100),
            //            Footer = TestBase.GenerateRandomString(100)
            //        });
            //    }
            //    if (format == "excel")
            //    {
            //        writeGroupsToExcelFile(groups, filename);
            //    }
            //    else
            //    {
            //        StreamWriter writer = new StreamWriter(filename);
            //        if (format == "csv")
            //        {
            //            writeGroupsToCsvFile(groups, writer);
            //        }
            //        else if (format == "xml")
            //        {
            //            writeGroupsToXmlFile(groups, writer);
            //        }
            //        else if (format == "json")
            //        {
            //            writeGroupsToJsonFile(groups, writer);
            //        }
            //        else
            //        {
            //            System.Console.Out.Write("Unrecognized format " + format);
            //        }

            //        writer.Close();
            //    }

            //}

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
