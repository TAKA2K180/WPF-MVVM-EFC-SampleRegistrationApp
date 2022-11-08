using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfRegistration.Domain.Models;

namespace WpfRegistration.Domain.Services
{
    public interface IUserDataService : IDataService<UserModel>
    {
        Task<UserModel> GetUser(string FirstName, string LastName, string Address, string VaccnineName, DateTime DateFirstDose);
    }
}
