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
            Init();
            
            var db = new SubjectRepository();

            var subjects = db.GetRelatedData();

            foreach (var s in subjects)
            {
                Console.WriteLine(s);
                Console.WriteLine();
            }
        }

        private static void Init()
        {
            var init = new SchoolContext();
            
            MyDatalnitializer.RecreateDatabase(init);
            
            MyDatalnitializer.InitializeData(init);
        }
    }
}