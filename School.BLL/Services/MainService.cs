using School.DAL;
using School.DAL.DataInitialization;

namespace School.BLL.Services
{
    public static class MainService
    {
        public static void Start()
        {
            var context = new SchoolDbContext();
            
            MyDataInitializer.RecreateDatabase(context);
            MyDataInitializer.InitializeData(context);
        }   
    }
}