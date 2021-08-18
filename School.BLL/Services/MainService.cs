using School.DAL;
using School.DAL.DataInitialization;

namespace School.BLL.Services
{
    public class MainService
    {
        private SchoolDbContext context;

        public MainService()
        {
            context = new SchoolDbContext();
            
            MyDataInitializer.RecreateDatabase(context);
            MyDataInitializer.InitializeData(context);
        }

        public SchoolDbContext GetDb() => context;
    }
}