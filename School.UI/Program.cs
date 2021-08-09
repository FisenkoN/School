using System;
using School.DAL.Datalnitialization;
using School.DAL.EF;
using School.DAL.EF.Repository;
using School.DAL.Repository;

namespace School.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            var classRepository = new ClassRepository();

            foreach (var @class in classRepository.GetRelatedData()) Console.WriteLine(@class);
        }

        private static void Init()
        {
            var context = new SchoolDbContext();
            
            MyDatalnitializer.RecreateDatabase(context);
            MyDatalnitializer.InitializeData(context);
        }
    }
}