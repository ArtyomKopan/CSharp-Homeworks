using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsGrades.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace StudentsGrades
{
    public class Program
    {
        /// <summary>
        /// Build and run application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            
            var app = builder.Build();

            app.Run();
        }

        /// <summary>
        /// Create application builder with services from file Startup.cs
        /// </summary>
        /// <param name="args"></param>
        /// <returns> Host application builder </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
