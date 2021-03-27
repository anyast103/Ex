using System;
using System.Collections.Generic;
using System.IO;

namespace Ex
{
    class Program
    {
        public static string[] information = File.ReadAllLines("tsk.txt"); // Получаем массив строк из файла.
        static void Main(string[] args)
        {
            new Person();
            new Salaries();
            Console.WriteLine("Среднее арифметическое зарплаты всех сотрудников, которые не являются директорами: " + Salaries.sal);
        }
    }
    class Person
    {
        public static List<List<string>> allPeople = new List<List<string>>(); // Скидываем сюда ВСЮ информацию о каждом человеке,
        // при этом список будет содержать ещё списоки, в которые либо 3, либо 4 элемента. ФИО, Цех, зарплата, директор или нет.
        
        public static List<string> otherPeople = new List<string>(); // Сюда скидываем оставшихся людей, которые не являются
        // директорами
        private static int directors { get; set; } // Ведём подсчёт количества директоров

        // Поиск директоров и их количества отношу к классу Person, так как это основной критерий.
        // Зарплату выношу в отельный класс, это касается уже просто денег.

        public Person()
        {
            GetInfo();
            DirectorOrNot();
            CountDirectors();
        }
        private static void GetInfo()
        {
            foreach (string i in Program.information)
            {
                List<string> onePerson = new List<string>();
                onePerson.AddRange(i.Split(';'));
                allPeople.Add(onePerson); 
            }
        }
        private static void DirectorOrNot()
        {
            foreach(var i in allPeople)
            {
                if(i.Contains("true")) // Если элемент списка списков (то есть еще один список включает в себя поле true,
                    // добавляем единичку к переменной подсчёта директоров.
                {
                    directors++;
                }
                else // Иначе кидаем в список простых рабочих.
                {
                    otherPeople.AddRange(i);
                }
            }
        }
        private static void CountDirectors()
        {
            if(directors > 2 || directors < 1) // Если директоров больше чем два, или их нет вообще, кидаем экспшен.
            {
                throw new Exception("Информация некорректна.");
            }
        }
    }
    class Salaries
    {
        public static int sal { get; private set; } // Кидаем сюда подсчитанную з.п
        public Salaries()
        {
            CalcTheAverageSal();
        }
        private static int CalcTheAverageSal()
        {
            foreach(var i in Person.allPeople)
            {
                if(!i.Contains("true")) // выбираем людей, которые НЕ директора.
                {
                    for(int index = 0; index < i.Count; index++)
                    {
                        sal += int.Parse(i[2]); // тянем из списка зарплаты, складываем.
                    }
                    return sal/i.Count; // Вычисляем среднее значение.
                }
            }
            return sal;
        }

    }
}
