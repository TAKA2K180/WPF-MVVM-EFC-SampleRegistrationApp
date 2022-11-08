using System;
using System.Linq;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;
using WpfRegistration.EntityFramework.Services;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataService<UserModel> dataService = new GenericDataService<UserModel>(new WpfRegistration.EntityFramework.DbContextFactory());
            Console.WriteLine(dataService.Delete(new Guid("7E0AC2D9-6C74-41D4-91A8-08DAB6F4BD65")).Result);
            //dataService.Create(new UserModel { Username = "Test" }).Wait();
        }
    }
}
