using System;
using System.Linq;
using System.Text;
using School.DAL.Datalnitialization;
using School.DAL.EF;
using School.DAL.EF.Repository;

namespace School.Production
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            var db = new StudentRepository();

            Console.WriteLine();


        }

        private static void Init()
        {
            var init = new SchoolContext();
            
            MyDatalnitializer.RecreateDatabase(init);
            
            MyDatalnitializer.InitializeData(init);
        }

        static void ShowMainMenu()
        {
          
        }
    }
}