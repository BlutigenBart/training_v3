using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Mapping;
using WebAddressbookTests;

namespace addressbook_tests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    //IEquatable<GroupData> // класс можно сравнивать с другими объектами GroupData
    //IComparable<GroupData> // можно сранивать с другими объектами GroupData
    {
        [Column(Name = "group_name")]
        public string Name { get; set; }
        [Column(Name = "group_header")]
        public string Header { get; set; }
        [Column(Name = "group_footer")]
        public string Footer { get; set; }
        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }
        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            { // С конструкцией using метод close вызывается автоматически в конце
                return (from g in db.Groups select g).ToList();
            }
        }
        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id)// && c.Deprecated == "0000-00-00 00:00:00")
                        select c).Distinct().ToList();

            }
        }
        public GroupData()
        {
        }

        public GroupData(string name)
        {
            Name = name;
        }

        public bool Equals (GroupData other)
        {
            if (Object.ReferenceEquals(other, null)) //если тот объект с которым мы сравниваем это null
            {
                return false; //возвращаем ложь, так как текущий объект есть и точно не равен тому который null
            }
            if (Object.ReferenceEquals(this, other)) //две ссылки указывают на 1 и тот же объект тогда тру
            {
                return true; //проверки не нужны так, как сравниваем объект с самим собой
            }
            return Name == other.Name;
        }

        public override int GetHashCode() //Оптимизация сравнения
        { 
            return Name.GetHashCode(); 
        }

        public override string ToString()
        {
            return  "name=" + Name + "\nheader= " + Header + "\nfooter=" + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null)) //если второй объект с которым мы сравниваем равен null
            {
                return 1; //одназначно текущий объект больше
            }
            return Name.CompareTo(other.Name);// если не null можно сравнивать по смыслу
        }


        public GroupData(string name, string header, string footer)
        {
            Name = name;
            Header = header;
            Footer = footer;
        }

    }
}
