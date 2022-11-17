using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
    public class TracerViewModel : BaseViewModel
    {
        #region Variables

        private IServiceAgent _serviceAgent;
        private IDataService<UserModel> dataService = new GenericDataService<UserModel>(new DbContextFactory());
        private string filter;
        private LogEventHelpers logEventHelpers = new LogEventHelpers();
        private MsmqHelper msmqHelper = new MsmqHelper();
        private Navigator navigator = new Navigator();
        private IServiceAgent sa = new ServiceAgent();

        #endregion Variables

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

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged("IsActive"); }
        }

        #endregion Properties

        #region Constructor

        public TracerViewModel(IServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
            this.DeleteCommand = new CustomCommand(Delete);
            this._isActive = true;

            Task.Run(() => LoadUsers()).Wait();

            //LoadUsers();
        }

        #endregion Constructor

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
            var Userlist = await Task.WhenAll(dataService.Getall());
            List<UserModel> userModels = new List<UserModel>();
            foreach (var user in Userlist)
            {
                var userlisting = user.ToList();
                userModels = userlisting;
            }
            ObservableCollection<UserModel> users = new ObservableCollection<UserModel>(userModels);
            this.Users = users;
            this.IsActive = false;
        }

        public void Delete(dynamic obj)
        {
            DeleteItemAsync();
        }

        public async Task DeleteItemAsync()
        {
            MessageHelper.messageBody = "Are you sure you want to delete the selected user?";

            MessageBoxView messageBoxView = new MessageBoxView();
            messageBoxView.ShowDialog();

            //MessageViewModel messageViewModel = new MessageViewModel();

            if (MessageHelper.isYesClicked == true)
            {
                this.IsActive = true;
                await DeleteUserAsync();
                MessageHelper.isYesClicked = default;
                this.IsActive = false;
            }
        }

        public async Task DeleteUserAsync()
        {
            msmqHelper.SendMessage("Deleted Record " + IdHandlers.FirstName + " " + IdHandlers.LastName + "");
            logEventHelpers.LogEventMessageInfo("Record deleted:\nFirst Name: " + IdHandlers.FirstName + "\nLast Name: " + IdHandlers.LastName + "\nAddress: " + IdHandlers.Address + "\nVaccine: " + this.VaccineName + "");
            await dataService.Delete(IdHandlers.Id);
            ExceptionHelper.exceptionMessage = "User deleted.";
            ModalWindows modalWindows = new ModalWindows();
            modalWindows.ShowDialog();
            await LoadUsers();
        }

        #endregion Methods
    }
}