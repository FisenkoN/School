using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using School.DAL.Datalnitialization;
using School.DAL.EF;

namespace School.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Reboot();
            
            CreateHostBuilder(args).Build().Run();
        }

        private static void Reboot()
        {
            var context = new SchoolContext();
            
            MyDatalnitializer.RecreateDatabase(context);
            MyDatalnitializer.InitializeData(context);
            
            context.Dispose();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}