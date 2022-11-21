using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;
using WpfRegistration.EntityFramework;
using WpfRegistration.EntityFramework.Services;
using WpfRegistrationApp.WPF.Commands;
using WpfRegistrationApp.WPF.State.Helpers;
using WpfRegistrationApp.WPF.State.Navigators;
using WpfRegistrationApp.WPF.Views;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class TracerEditViewModel : BaseViewModel
    {
        #region Variables

        private readonly IDataService<UserModel> dataService = new GenericDataService<UserModel>(new DbContextFactory());
        private readonly UserModel user = new UserModel();
        private readonly LogEventHelpers LogEventHelpers = new LogEventHelpers();
        private readonly MsmqHelper msmqHelper = new MsmqHelper();
        private readonly INavigator _navigator = new Navigator();

        private readonly int passCounter = 0;

        #endregion Variables

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

        private DateTime _dateFirstDose = DateTime.Now;

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

        public CustomCommand SubmitCommand
        { get; set; }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; OnPropertyChanged("IsEnabled"); }
        }

        public ErrorViewModel ErrorMessageViewModel { get; }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged("IsActive"); }
        }

        #endregion Properties

        #region Constructor

        public TracerEditViewModel()
        {

            SubmitCommand = new CustomCommand(this.Update);

           // Task.Run(() => LoadUserAsync()).Wait();
            LoadUserAsync();
            
            
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);

            ErrorMessageViewModel = new ErrorViewModel();
        }

        #endregion Constructor

        #region Methods

        public void Update(dynamic obj)
        {
            if (this.FirstName == null || this.FirstName == "" && this.LastName == null || this.LastName == "" && this.Address == null && this.UserName == null)
            {
                ExceptionHelper.exceptionMessage = "Please fill up required fields";
                ModalWindows modalWindows = new ModalWindows();
                modalWindows.ShowDialog();
            }
            else
            {
                MessageHelper.messageBody = "Are you sure you want to update with the following details?";
                MessageBoxView messageBoxView = new MessageBoxView();
                messageBoxView.ShowDialog();

                if (MessageHelper.isYesClicked == true)
                {
                    this.IsActive = true;

                    UpdateAsync();
                    MessageHelper.isYesClicked = default;
                    this.IsActive = false;
                }
            }
        }

        public async Task UpdateAsync()
        {
            if (this.Id != null)
            {
                await dataService.Update(this.Id, new UserModel()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Username = this.UserName,
                    Email = this.Email,
                    Address = this.Address,
                    DateFirstDose = this.DateFirstDose,
                    NumberofShots = this.NumberofShots,
                    VaccineName = this.VaccineName,
                    isBoosterShot = this.IsBooster,
                    isVaccinated = this.IsVaccinated,
                    DateJoined = IdHandlers.DateJoined
                });
                if (ExceptionHelper.isExceptionHandled == false)
                {
                    ExceptionHelper.exceptionMessage = "Profile updated.";
                    ModalWindows modalWindows = new ModalWindows();
                    modalWindows.ShowDialog();
                    msmqHelper.SendMessage($"Record updated {this.FirstName} {" "} {this.LastName}");
                    LogEventHelpers.LogEventMessageInfo($"Record updated:\nFirst Name: {this.FirstName}\nLast Name: {this.LastName}\nAddress: {this.Address}\nVaccine: {this.VaccineName}");
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
        //private void NotifyError(string message, Exception error)
        //{
        //    if (message == "Loaded")
        //    {
        //        //LogEventHelpers.LogEventMessageInfo(message);
        //    }
        //    else
        //    {
        //        LogEventHelpers.LogEventMessageError(message);
        //    }
        //}
        public async Task LoadUserAsync()
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
                        ExceptionHelper.exceptionMessage = "Please select a user first in Tracer info menu";
                        ModalWindows modalWindows = new ModalWindows();
                        modalWindows.ShowDialog();
                        this.IsEnabled = false;
                    }
                }
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            OnExit();
        }

        public void OnExit()
        {
            this.FirstName = null;
            this.LastName = null;
            this.Address = null;
            this.Email = null;
            this.UserName = null;
            this.DateFirstDose = default;
            this.NumberofShots = default;
            this.VaccineName = null;
            this.IsBooster = default;
            this.IsVaccinated = default;
            this.IsEnabled = default;
            this.Id = default;

            IdHandlers.Id = default;
            ExceptionHelper.exceptionCounter = 0;
        }

        #endregion Methods
    }
}