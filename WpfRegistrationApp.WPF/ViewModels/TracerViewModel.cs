using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;
using WpfRegistration.EntityFramework.Services;
using WpfRegistration.EntityFramework;
using System.Linq;
using WpfRegistrationApp.WPF.Commands;
using System.Threading.Tasks;
using System.Windows;
using WpfRegistrationApp.WPF.State.Navigators;
using WpfRegistrationApp.WPF.State;
using System.Windows.Input;
using System.Reflection;
using MSMQ.Messaging;
using System.Diagnostics;
using System.Windows.Data;
using WpfRegistrationApp.WPF.State.Helpers;
using WpfRegistrationApp.WPF.Views;
using MaterialDesignThemes.Wpf;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class TracerViewModel : BaseViewModel
    {
        #region Variables
        private IServiceAgent _serviceAgent;
        private ICollectionView phrasesView;
        IDataService<UserModel> dataService = new GenericDataService<UserModel>(new DbContextFactory());
        private string filter;
        LogEventHelpers logEventHelpers = new LogEventHelpers();
        MsmqHelper msmqHelper = new MsmqHelper(); 
        Navigator navigator = new Navigator();
        private readonly INavigator _navigator;
        IServiceAgent sa = new ServiceAgent();
        #endregion

        #region Properties
        private ObservableCollection<UserModel> _users;
        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged("Users"); }
        }
        public ICommand EditCommand { get; set; }

        private UserModel selectedUser;
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        private int selectedUserIndex;
        public int SelectedUserIndex
        {
            get { return selectedUserIndex; }
            set { selectedUserIndex = value; }
        }

        private Guid _id;

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

        public CustomCommand DeleteCommand { get; set; }

        public CustomCommand SearchCommand { get; set; }

        public DateTime DateFirstDose
        {
            get { return _dateFirstDose; }
            set { _dateFirstDose = value; OnPropertyChanged(Convert.ToString(DateFirstDose)); }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(UserName); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(UserName); }
        }

        private int _numberofShots;

        public int NumberofShots
        {
            get { return _numberofShots; }
            set { _numberofShots = value; }
        }

        private bool _isBooster;

        public bool IsBooster
        {
            get { return _isBooster; }
            set { _isBooster = value; }
        }

        private bool _isVaccinated;

        public bool IsVaccinated
        {
            get { return _isVaccinated; }
            set { _isVaccinated = value; }
        }
        public string Filter
        {
            get
            {
                return filter;
            }
            set
            {
                if (value != filter)
                {
                    filter = value;
                    phrasesView.Refresh();
                    OnPropertyChanged("Filter");
                }
            }
        }

        private CustomCommand _nextPage;
        public CustomCommand NextPage
        {
            get { return _nextPage; }
            set { _nextPage = value; }
        }
        #endregion

        #region Constructor
        public TracerViewModel(IServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            this.DeleteCommand = new CustomCommand(Delete);
            LoadUsers(); 
        }
        #endregion

        #region Methods
        public async Task LoadUsers()
        {
            //_serviceAgent.GetUsers((_users, error) => UserLoaded(_users, error));
            await GetAllUserlist();
        }
        private void UserLoaded(ObservableCollection<UserModel> users, Exception error)
        {
            if (error == null)
            {
                this.Users = users;
                NotifyError("Loaded", null);
            }
            else
            {
                NotifyError(error.Message, error);
            }
        }
        private void NotifyError(string message, Exception error)
        {
            if (message == "Loaded")
            {
                //logEventHelpers.LogEventMessageInfo(message);
            }
            else
            {
                logEventHelpers.LogEventMessageError(message);
            }
        }

        public void SelectedItem(UserModel user)
        {
            if (user != null)
            {
                IdHandlers.Id = user.Id;
                IdHandlers.FirstName = user.FirstName;
                IdHandlers.LastName = user.LastName;
                IdHandlers.Address = user.Address;
                IdHandlers.Username = user.Username;
                IdHandlers.Email = user.Email;
                IdHandlers.VaccineName = user.VaccineName;
                IdHandlers.DateFirstDose = user.DateFirstDose;
                IdHandlers.NumberofShots = user.NumberofShots;
                IdHandlers.isVaccinated = user.isVaccinated;
                IdHandlers.isBoosterShot = user.isBoosterShot;
            }
        }

        public async Task GetAllUserlist()
        {
            await GetUsersAsync();
        }

        public async Task GetUsersAsync()
        {
            try
            {
                var Userlist = await Task.WhenAll(dataService.Getall());
                List<UserModel> userModels = new List<UserModel>();
                foreach (var user in Userlist)
                {
                    var userlisting = user.ToList();
                    userModels = userlisting;
                }
                ObservableCollection<UserModel> users = new ObservableCollection<UserModel>(userModels);
                this.Users = users;
            }
            catch (Exception ex)
            {
                logEventHelpers.LogEventMessageError(ex.ToString());
            }
        }
        public void Delete(dynamic obj)
        {
            DeleteItem();
        }
        public async Task DeleteItem()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to the delete the selected user?", "WPF Tracer App", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        msmqHelper.SendMessage("Deleted Record " + IdHandlers.FirstName + " " + IdHandlers.LastName + "");
                       logEventHelpers.LogEventMessageInfo("Record deleted:\nFirst Name: " + IdHandlers.FirstName + "\nLast Name: " + IdHandlers.LastName + "\nAddress: " + IdHandlers.Address + "\nVaccine: " + this.VaccineName + "");
                        await dataService.Delete(IdHandlers.Id);
                        MessageBox.Show("User deleted.", "WPF Tracer App", MessageBoxButton.OK, MessageBoxImage.Information);
                        await LoadUsers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        logEventHelpers.LogEventMessageError(ex.ToString());
                    }
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        public void OnClick()
        {
        }
            
        #endregion
    }
}
