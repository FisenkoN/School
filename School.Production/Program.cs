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
                var studentYura = new Student
                {
                    Age = 14, ClassId = 5, FirstName = "Yura", LastName = "Fisenko", Gender = Gender.Male, Subjects =
                        new List<Subject>
                        {
                            new Subject
                            {
                                Name = "Ukrainian", Teachers = new List<Teacher>
                                {
                                    new Teacher
                                    {
                                        FirstName = "Olya", LastName = "Polakova", Age = 34, Gender = Gender.Female
                                    }

                                }
                            }
                        }
                };
                
                repo.Add(studentYura);
                
                // foreach (var student in repo.GetAll(s=>s.FirstName, false))
                // {
                //     Console.WriteLine(student.FullName);
                // }

                Console.WriteLine(repo.GetSome(s=>s.FirstName == "Yura" && s.LastName == "Fisenko").First());

                repo.Delete(studentYura);

                if (repo.GetSome(s => s.FirstName == "Yura" && s.LastName == "Fisenko").Count == 0)
                {
                    Console.WriteLine("NotFound");
                }
                else
                {
                    Console.WriteLine(repo.GetSome(s => s.FirstName == "Yura" && s.LastName == "Fisenko").First());
                }

                // studentYura.Age = 17;
                //
                // studentYura.Subjects.Add(new Subject{Name = "France", Teachers = new List<Teacher>{new Teacher{FirstName = "Inna", LastName = "Denko", Age = 40, Gender = Gender.Female}}});
                //
                // studentYura.ClassId = 1;
                //
                // repo.Update(studentYura);
                //
                // Console.WriteLine(repo.GetSome(s=>s.FirstName == "Yura" && s.LastName == "Fisenko").First());
            }
        }
    }
}