﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

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
            // Если фамилии роазные возвращает результат их сравнения
            int lastNameCompare = Lastname.CompareTo(other.Lastname);
            if (lastNameCompare != 0)
            {
                return lastNameCompare; //если фамилии не одинаковы, возвращает результат их сравнения
            }
            //если фамилии одинаковы, переходит к сравнению имен
            return Firstname.CompareTo(other.Firstname);
        }

        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Home { get; set; }
        public string Mobile { get; set; }
        public string Work { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }
        public string Bday { get; set; }
        public string Bmonth { get; set; }
        public string Byear { get; set; }
        public string Aday { get; set; }
        public string Amonth { get; set; }
        public string Ayear { get; set; }
        public string New_group { get; set; }
        public string Id { get; set; }
    }
}
