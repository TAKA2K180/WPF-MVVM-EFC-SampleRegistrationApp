using System;
using System.Collections.Generic;
using System.Linq;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;
using WpfRegistration.EntityFramework;
using WpfRegistration.EntityFramework.Services;

namespace WpfRegistration.ConsoleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataService<UserModel> dataService = new GenericDataService<UserModel>(new DbContextFactory());

            //dataService.Create(new UserModel { FirstName = "Rheinel Roi", LastName = "Manalo", Username = "rmanalo", DateJoined = DateTime.Now, Id = new Guid(), Email = "rmanalo@intercardinc.com" }).Wait();
            List<UserModel> users = new List<UserModel>();
            foreach (var user in users)
            {
                Console.WriteLine(user.FirstName, user.LastName);
            }


            //Console.WriteLine(dataService.Getall().Result.FirstOrDefault());
        }
    }
}
