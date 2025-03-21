using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests
{
    public class GroupData : IEquatable<GroupData> //класс можно сравнивать с другими объектами GroupData 
    {
        private string name;
        private string header = "";
        private string footer = "";

        public GroupData(string name)
        {
            this.name = name;
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

        public int GetHashCode() //Оптимизация сравнения
        { 
            return Name.GetHashCode(); 
        }


        public GroupData(string name, string header, string footer)
        {
            this.name = name;
            this.name = header;
            this.name = footer;
        }

        public string Name
        {
            get // возвращает значение поля
            {
                return name;
            }
            set // присваивает значение
            {
                name = value;
            }
        }

        public string Header
        {
            get // возвращает значение поля
            {
                return header;
            }
            set // присваивает значение
            {
                header = value;
            }
        }

        public string Footer
        {
            get // возвращает значение поля
            {
                return footer;
            }
            set // присваивает значение
            {
                footer = value;
            }
        }
    }
}
