using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests
{
    class Square : Figure //квадрат
    {
        private int size;

        public Square(int size)
        {
            this.size = size;
        }
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        // Методы getSize и setSize больше не нужны из-за метода  public int Size
        //public int getSize()
        //{
        //    return size;
        //}

        //public void setSize(int size)
        //{
        //    this.size = size;
        //}
    }
}
