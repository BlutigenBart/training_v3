using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    //IEquatable<GroupData> // класс можно сравнивать с другими объектами GroupData
    //IComparable<GroupData> // можно сранивать с другими объектами GroupData
    {

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
            return  "name" + Name;
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

        public string Name { get; set; }
        
        public string Header { get; set; }

        public string Footer { get; set; }

        public string Id { get; set; }

    }
}
