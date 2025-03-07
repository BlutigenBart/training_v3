using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests
{
    public class GroupData
    {
        private string name;
        private string header = "";
        private string footer = "";

        public GroupData(string name)
        {
            this.name = name;
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
