using System;
using System.Collections.Generic;
using System.Linq;
using School.DAL.Datalnitialization;
using School.DAL.EF;
using School.DAL.EF.Repository;
using School.Models;

namespace School.Production
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** PRODUCTION *****\n");
            using (var context = new SchoolContext())
            {
                MyDatalnitializer.RecreateDatabase(context);
                MyDatalnitializer.InitializeData(context);
                
            }

            using (var repo = new StudentRepository())
            {
                repo.Add(new Student{Age = 14, ClassId = 5, FirstName = "Yura", LastName = "Fisenko", Gender = Gender.Male, Subjects = new List<Subject>
                {new Subject{Name = "Ukrainian", Teachers = new List<Teacher>
                {
                    new Teacher{FirstName = "Olya", LastName = "Polakova", Age = 34, Gender = Gender.Female}
                            
                }}}});
                
                // foreach (var student in repo.GetAll())
                // {
                //     Console.WriteLine(student.FullName);
                // }

                //Console.WriteLine(repo.GetSome(s=>s.FirstName == "Yura" && s.LastName == "Fisenko").First());
            }
        }
    }
}