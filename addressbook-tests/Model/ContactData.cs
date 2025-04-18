using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;
using LinqToDB;
using LinqToDB.Mapping;
using WebAddressbookTests;

namespace addressbook_tests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        [Column(Name = "firstname")]
        public string Firstname { get; set; }
        [Column(Name = "middlename")]
        public string Middlename { get; set; }
        [Column(Name = "lastname")]
        public string Lastname { get; set; }
        [Column(Name = "nickname")]
        public string Nickname { get; set; }
        public string Photo { get; set; }
        [Column(Name = "title")]
        public string Title { get; set; }
        [Column(Name = "company")]
        public string Company { get; set; }
        [Column(Name = "address")]
        public string Address { get; set; }
        [Column(Name = "home")]
        public string Home { get; set; }
        [Column(Name = "mobile")]
        public string Mobile { get; set; }
        [Column(Name = "work")]
        public string Work { get; set; }
        [Column(Name = "fax")]
        public string Fax { get; set; }
        [Column(Name = "email")]
        public string Email { get; set; }
        [Column(Name = "email2")]
        public string Email2 { get; set; }
        [Column(Name = "email3")]
        public string Email3 { get; set; }
        [Column(Name = "homepage")]
        public string Homepage { get; set; }
        [Column(Name = "bday")]
        public string Bday { get; set; }
        [Column(Name = "bmonth")]
        public string Bmonth { get; set; }
        [Column(Name = "byear")]
        public string Byear { get; set; }
        [Column(Name = "aday")]
        public string Aday { get; set; }
        [Column(Name = "amonth")]
        public string Amonth { get; set; }
        [Column(Name = "ayear")]
        public string Ayear { get; set; }
        public string New_group { get; set; }
        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }
        private string allPhones;
        private string allEmails;
        private string fullName;
        private string allInformationOnDetails;
        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUpPhone(Home) + CleanUpPhone(Mobile) + CleanUpPhone(Work)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanUpPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            // Меняем символы на пустые строки
            //return Regex.Replace(phone, "[ -()]", "") + "\r\n";
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").
                Replace(")", "").Replace("H:", "").Replace("M:", "").
                Replace("W:", "").Replace("\r", "").Replace("\n", "") + "\r\n";
        }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            // Меняем символы на пустые строки
            return email.Replace(" ", "").Replace("(", "").Replace(")", "")
                .Replace("\r", "").Replace("\n", "") + "\r\n";
        }
        public string FullName
        {
            get
            {
                if (fullName != null)
                {
                    return fullName;
                }
                else
                {
                    return (CleanUpNames(Firstname) + CleanUpNames(Middlename) + CleanUpNames(Lastname)).Trim();
                }

            }
            set => fullName = value;
        }
        //идет как Имя, Отчество, Фамилия
        //нужно вытащить отдельно Имя и Фамилию из примера текста (Борис Андреевич Моисеев)
        private string CleanUpNames(string names)
        {
            if (names == null || names == "")
            {
                return "";
            }
            else
            {

            }
            // Меняем символы на пустые строки
            return names.Replace("(", "").Replace(")", "")
                .Replace("\r", "").Replace("\n", "")
                .Replace("<br>", "")
                + " ";
        }

        public string AllInformationOnDetails
        {
            get
            {


                if (allInformationOnDetails != null)
                {
                    return allInformationOnDetails;
                }
                else
                {
                    string homeTel = "";
                    string mobileTel = "";
                    string workTel = "";
                    string faxW = "";
                    string homePages = "";
                    if (CleanUpDetails(Home) != "")
                    {
                        homeTel = "H: " + CleanUpDetails(Home);
                    }
                    if (CleanUpDetails(Mobile) != "")
                    {
                        mobileTel = "M: " + CleanUpDetails(Mobile);
                    }
                    if (CleanUpDetails(Work) != "")
                    {
                        workTel = "W: " + CleanUpDetails(Work);
                    }
                    if (CleanUpDetails(Fax) != "")
                    {
                        faxW = "F: " + CleanUpDetails(Fax);
                    }
                    if (CleanUpDetails(Homepage) != "")
                    {
                        homePages = "Homepage:\r\n" + CleanUpDetails(Homepage);
                    }

                    return (CleanUpDetails(Nickname) +
                        CleanUpDetails(Title) +
                        CleanUpDetails(Company) +
                        CleanUpDetails(Address) +
                        homeTel +
                        mobileTel +
                        workTel +
                        faxW +
                        CleanUpDetails(Email) +
                        CleanUpDetails(Email2) +
                        CleanUpDetails(Email3) +
                        homePages.Trim()
                        ).TrimEnd('\r', '\n');
                }

            }
            set => allInformationOnDetails = value;
        }

        private string CleanUpDetails(string details)
        {
            if (details == null || details == "")
            {
                return "";
            }
            else
            {

            }
            // Меняем символы на пустые строки
            return details.Replace("(", "").Replace(")", "")
                .Replace("\r", "").Replace("\n", "").Replace("<br>", "")
                 + "\r\n";
        }
        /// <summary>
        /// Полный список всех контактов из урока 7.4
        /// </summary>
        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            { // С конструкцией using метод close вызывается автоматически в конце
                return (from c in db.Contacts select c).ToList();
            }
        }

        //public static List<ContactData> GetAll()
        //{
        //    using (AddressBookDB db = new AddressBookDB())
        //    { // С конструкцией using метод close вызывается автоматически в конце
        //        return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
        //    }
        //}

        //public List<ContactData> GetContacts()
        //{
        //    using (AddressBookDB db = new AddressBookDB())
        //    {
        //        return (from c in db.Contacts
        //                from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id)
        //                select c).Distinct().ToList();

        //    }
        //}

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public ContactData() { }
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null)) //если тот объект с которым мы сравниваем это null
            {
                return false; //возвращаем ложь, так как текущий объект есть и точно не равен тому который null
            }
            if (Object.ReferenceEquals(this, other)) //две ссылки указывают на 1 и тот же объект тогда тру
            {
                return true; //проверки не нужны так, как сравниваем объект с самим собой
            }
            return Lastname == other.Lastname && Firstname == other.Firstname;

        }
        public override int GetHashCode() //Оптимизация сравнения
        {
            return HashCode.Combine(Lastname, Firstname);
        }
        public override string ToString()
        {
            return "lastname" + Lastname + " firstname" + Firstname;
        }
        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null)) //равен ли второй объект null
            {
                return 1; //если второй объект равен null, возвращает 1. текущий объект больше null
            }
            //return (Firstname + Lastname).CompareTo(other.Firstname + other.Lastname);// если не null можно сравнивать по смыслу

            // Если фамилии роазные возвращает результат их сравнения
            int lastNameCompare = Lastname.CompareTo(other.Lastname);
            if (lastNameCompare != 0)
            {
                return lastNameCompare; //если фамилии не одинаковы, возвращает результат их сравнения
            }
            //если фамилии одинаковы, переходит к сравнению имен
            return Firstname.CompareTo(other.Firstname);
        }

    }

}
