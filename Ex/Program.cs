using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ex
{
    class Program
    {
        static void Main(string[] args)
        {
            DataReader.GetPerson();
            WorkPlace.GetWorkPlaceInfo();
            
        }
    }
    class DataReader
    {
        public static string[] information { get; private set; } = File.ReadAllLines("tsk.txt");

        public static List<Person> person = new List<Person>();

        public static void GetPerson()
        {
            foreach(var employee in information)
            {
                person.Add(new Person(employee.Split(';'), new WorkPlace(employee.Split(';')[1])));
            }
        }
    }
    class Person
    {
        public string Name { get; private set; }
        public WorkPlace Work { get; private set; }
        public int Salary { get; private set; }
        public bool IsHead { get; private set; } = false;

        public Person(string[] inforamtion, WorkPlace work)
        {
            Name = inforamtion[0];
            Work = work;
            Salary = int.Parse(inforamtion[2]);
            if (inforamtion.Length == 4)
                IsHead = Convert.ToBoolean(inforamtion[3]);
            else IsHead = false;
        }
    }
    class WorkPlace
    {
        public string workName { get; private set; }
        
        public WorkPlace(string work)
        {
            workName = work;
        }

        public static void GetWorkPlaceInfo()
        {
            var groupOfemployees = DataReader.person.GroupBy(e => e.Work.workName, (key, g) => 
            new { Key = key, Value = g.Count(f => f.IsHead == true) }).OrderBy(c => c.Key);
            foreach (var employee in groupOfemployees)
            {
                if (employee.Value < 1 || employee.Value > 2)
                    throw new Exception("Ошибка!");
            }
            Salaries.GetSalaries();
            Salaries.GetTheRichestHead();
        }
    }
    class Salaries
    {
        public static void GetSalaries()
        {

            var groupOfEmployees = DataReader.person.GroupBy(e => e.Work.workName,
                (key, g) => new { Key = key, Value = g.Where(g => g.IsHead == false) }
                );
            foreach (var employee in groupOfEmployees)
            {
                double salary = employee.Value.Average(f => f.Salary);
                Console.WriteLine($"Средняя зарплата по {employee.Key}: " + salary);
            }
        }
        public static void GetTheRichestHead()
        {
            var groupOfHeads = DataReader.person.GroupBy(e => e.Work.workName,
                (key, g) => new { Key = key, Value = g.Where(g => g.IsHead == true) }
                );
            List<double> headSalaries = new List<double>();
            foreach (var head in groupOfHeads)
            {
                headSalaries.Add(head.Value.Max(f => f.Salary));
            }
            var richest = headSalaries.Max();
            Console.WriteLine($"Cамая высокая зарплата руководителя: " + richest);
        }
    }
    
}
                
            
    

