using MSMQ.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;
using WpfRegistration.EntityFramework;
using WpfRegistration.EntityFramework.Services;
using WpfRegistrationApp.WPF.Commands;
using WpfRegistrationApp.WPF.State;
using WpfRegistrationApp.WPF.State.Helpers;
using WpfRegistrationApp.WPF.State.Navigators;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class TracerEditViewModel : BaseViewModel
    {
        #region Variables
        private IServiceAgent _serviceAgent;
        IDataService<UserModel> dataService = new GenericDataService<UserModel>(new DbContextFactory());
        UserModel user = new UserModel();
        LogEventHelpers LogEventHelpers = new LogEventHelpers();
        MsmqHelper msmqHelper = new MsmqHelper();
        INavigator _navigator = new Navigator();
        int passCounter = 0;
        #endregion

        #region Properties
        private ObservableCollection<UserModel> _users;
        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged("Users"); }
        }

        private Guid _id = IdHandlers.Id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("ID"); }
        }
        

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }
        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }
        private string _vaccineName;
        public string VaccineName
        {
            get { return _vaccineName; }
            set { _vaccineName = value; OnPropertyChanged(nameof(VaccineName)); }
        }

        private DateTime _dateFirstDose;

        public DateTime DateFirstDose
        {
            get { return _dateFirstDose; }
            set { _dateFirstDose = value; OnPropertyChanged("DateFirstDose"); }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(nameof(UserName)); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        private int _numberofShots;

        public int NumberofShots
        {
            get { return _numberofShots; }
            set { _numberofShots = value; OnPropertyChanged("NumberofShots"); }
        }

        private bool _isBooster;

        public bool IsBooster
        {
            get { return _isBooster; }
            set { _isBooster = value; OnPropertyChanged("IsBooster"); }
        }

        private bool _isVaccinated;

        public bool IsVaccinated
        {
            get { return _isVaccinated; }
            set { _isVaccinated = value; OnPropertyChanged("IsVaccinated"); }
        }

        private CustomCommand _updateCommand;
        public CustomCommand SubmitCommand
        { get; set; }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; OnPropertyChanged("IsEnabled"); }
        }

        #endregion

        #region Constructor
        public TracerEditViewModel(IServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SubmitCommand = new CustomCommand(this.Update);

            LoadUserAsync();
        }
        #endregion

        #region Methods
        public void Update(dynamic obj)
        {
            if (this.FirstName == null || this.FirstName == "" && this.LastName == null || this.LastName == "" && this.Address == null && this.UserName == null )
            {
                MessageBox.Show("Please fill up required fields", "WPF Tracer App", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure about the information you have entered?", "WPF Tracer App", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        try
                        {
                            dataService.Update(this.Id, new UserModel() { FirstName = this.FirstName, LastName = this.LastName, Username = this.UserName, Email = this.Email, Address = this.Address, DateFirstDose = this.DateFirstDose, NumberofShots = this.NumberofShots, VaccineName = this.VaccineName, isBoosterShot = this.IsBooster, isVaccinated = this.IsVaccinated });
                            MessageBox.Show("Profile updated");
                            msmqHelper.SendMessage("Record updated " + this.FirstName + " " + this.LastName + "");
                            LogEventHelpers.LogEventMessageInfo("Record updated:\nFirst Name: " + this.FirstName + "\nLast Name: " + this.LastName + "\nAddress: " + this.Address + "\nVaccine: " + this.VaccineName + "");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            LogEventHelpers.LogEventMessageError(ex.Message);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
        }
        //public void LoadUserById()
        //{
        //    try
        //    {
        //        _serviceAgent.GetUserbyId((_users, error) => UserLoaded(_users, error));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogEventHelpers.LogEventMessageError(ex.Message);
        //    }
        //}
        //private void UserLoaded(ObservableCollection<UserModel> users, Exception error)
        //{
        //    if (error == null)
        //    {
        //        this.Users = users;
        //        NotifyError("Loaded", null);
        //        LoadUser();
        //    }
        //    else
        //    {
        //        NotifyError(error.Message, error);
        //    }
        //}
        private void NotifyError(string message, Exception error)
        {
            if (message == "Loaded")
            {
                //LogEventHelpers.LogEventMessageInfo(message);
            }
            else
            {
                LogEventHelpers.LogEventMessageError(message);
            }
        }
        public async Task LoadUserAsync()
        {
            try
            {
                var getId = await Task.WhenAll(dataService.Get(IdHandlers.Id));
                List<UserModel> users = new List<UserModel>();
                users = getId.ToList();
                ExceptionHelper.exceptionCounter++;
                foreach (var user in users)
                {
                    if (user != null)
                    {
                        this.FirstName = user.FirstName;
                        this.LastName = user.LastName;
                        this.Address = user.Address;
                        this.Email = user.Email;
                        this.UserName = user.Username;
                        this.DateFirstDose = user.DateFirstDose;
                        this.NumberofShots = user.NumberofShots;
                        this.VaccineName = user.VaccineName;
                        this.IsBooster = user.isBoosterShot;
                        this.IsVaccinated = user.isVaccinated;
                        this.IsEnabled = true;
                    }
                    else
                    {
                        if (ExceptionHelper.exceptionCounter <= 1)
                        {
                            MessageBox.Show("Please select a user first in Tracer Info menu", "WPF Tracer App", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.IsEnabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogEventHelpers.LogEventMessageError(ex.Message);
            }
        }

        public void Onexit()
        {
            if (_navigator.currentViewmodel != new TracerEditViewModel(_serviceAgent))
            {

            }
        }
        #endregion
    }
}
