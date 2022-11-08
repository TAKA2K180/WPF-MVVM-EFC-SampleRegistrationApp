using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfRegistration.Domain.Models;

namespace WpfRegistration.EntityFramework.Services
{
    public interface IServiceAgent
    {
        void GetUsers(Action<ObservableCollection<UserModel>, Exception> completed);
        void GetUserbyId(Action<ObservableCollection<UserModel>, Exception> completed);
    }
    public class ServiceAgent : IServiceAgent
    {
        public void GetUsers(Action<ObservableCollection<UserModel>, Exception> completed)
        {
            try
            {
                ObservableCollection<UserModel> users = AlternateDataService.FetchUsers();
                completed(users, null);
            }
            catch (Exception ex)
            {
                completed(null, ex);
            }
        }

        public void GetUserbyId(Action<ObservableCollection<UserModel>, Exception> completed)
        {
            try
            {
                ObservableCollection<UserModel> users = AlternateDataService.FetchUserbyId();
                completed(users, null);
            }
            catch (Exception ex)
            {
                completed(null, ex);
            }
        }
    }
}
