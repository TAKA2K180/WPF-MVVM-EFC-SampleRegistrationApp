using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WpfRegistration.Domain.Models;

namespace WpfRegistration.Domain.Services
{
    public interface IUserService : IDataService<UserModel>
    {
        Task<UserModel> GetByUsername(string username);
        Task<UserModel> GetByEmail(string email);
    }
}
