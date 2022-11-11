using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfRegistration.Domain.Models;

namespace WpfRegistration.EntityFramework.Services
{
    public static class AlternateDataService
    {
        public static ObservableCollection<UserModel> FetchUsers()
        {
            return AlternateDbContext.GetUsers();
        }

        public static ObservableCollection<UserModel> FetchUserbyId()
        {
            return AlternateDbContext.GetUsersById();
        }

        public static ObservableCollection<UserModel> FetchUserbySearch()
        {
            return AlternateDbContext.GetUsersBySearch();
        }
    }
}
