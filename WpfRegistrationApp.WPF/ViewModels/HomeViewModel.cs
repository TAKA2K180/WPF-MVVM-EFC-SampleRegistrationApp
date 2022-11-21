using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;
using WpfRegistration.EntityFramework;
using WpfRegistration.EntityFramework.Services;
using WpfRegistrationApp.WPF.Commands;
using WpfRegistrationApp.WPF.State.Helpers;
using WpfRegistrationApp.WPF.Views;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Variables

        private IDataService<UserModel> dataService = new GenericDataService<UserModel>(new DbContextFactory());
        public static readonly PasswordHelper PasswordHelper = new PasswordHelper();
        private LogEventHelpers logEventHelpers = new LogEventHelpers();
        private MsmqHelper msmqHelper = new MsmqHelper();

        #endregion Variables

        #region Properties

        public ObservableCollection<UserModel> Users { get; set; }

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
            set { _firstName = value; OnPropertyChanged(FirstName); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(LastName); }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(Address); }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(UserName); }
        }

        private string _emailAdd;

        public string EmailAdd
        {
            get { return _emailAdd; }
            set { _emailAdd = value; OnPropertyChanged(EmailAdd); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(Password); }
        }

        private string _vaccineName;

        public string VaccineName
        {
            get { return _vaccineName; }
            set { _vaccineName = value; OnPropertyChanged(VaccineName); }
        }

        private DateTime _dateJoined;

        public DateTime DateJoined
        {
            get { return _dateJoined; }
            set { _dateJoined = value; OnPropertyChanged(Convert.ToString(DateJoined)); }
        }

        private DateTime _dateFirstDose = DateTime.Now;

        public DateTime DateFirstDose
        {
            get { return _dateFirstDose; }
            set { _dateFirstDose = value; OnPropertyChanged(Convert.ToString(DateFirstDose)); }
        }

        private int _numberofDose;

        public int NumberofDose
        {
            get { return _numberofDose; }
            set { _numberofDose = value; OnPropertyChanged(Convert.ToString(NumberofDose)); }
        }

        private bool _isBoosterShot;

        public bool IsBoosterShot
        {
            get { return _isBoosterShot; }
            set { _isBoosterShot = value; OnPropertyChanged("Checked"); }
        }

        private bool _isVaccinated;

        public bool IsVaccinated
        {
            get { return _isVaccinated; }
            set { _isVaccinated = value; OnPropertyChanged("Checked"); }
        }

        private CustomCommand _insertCommand;

        public CustomCommand SubmitCommand
        { get; set; }

        #endregion Properties

        #region Constructor

        public HomeViewModel()
        {
            this.SubmitCommand = new CustomCommand(this.Create);
        }

        #endregion Constructor

        #region Methods

        public void Create(dynamic obj)
        {
            if (this.FirstName == String.Empty || this.FirstName == null && this.LastName == String.Empty || this.LastName == null && this.UserName == String.Empty || this.UserName == null && this.Address == String.Empty || this.Address == null)
            {
                ExceptionHelper.exceptionMessage = "Please fill up required fields";
                ModalWindows modalWindows = new ModalWindows();
                modalWindows.ShowDialog();
            }
            else
            {
                MessageHelper.messageBody = "Are you sure you want to register the following details?";

                MessageBoxView messageBoxView = new MessageBoxView();
                messageBoxView.ShowDialog();

                if (MessageHelper.isYesClicked == true)
                {
                    //var task = Task.Run(() => Insert());
                    Insert();
                    MessageHelper.isYesClicked = default;
                }
            }
        }

        public async Task Insert() 
        {
            await dataService.Create(new UserModel
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Username = this.UserName,
                DateJoined = DateTime.Now,
                Id = new Guid(),
                Email = this.EmailAdd,
                DateFirstDose = this.DateFirstDose,
                isBoosterShot = this.IsBoosterShot,
                isVaccinated = this.IsVaccinated,
                NumberofShots = this.NumberofDose,
                VaccineName = this.VaccineName,
                Address = this.Address
            });
            if (ExceptionHelper.isExceptionHandled == false)
            {
                ExceptionHelper.exceptionMessage = "Tracer record submitted";
                ModalWindows modalWindows = new ModalWindows();
                modalWindows.ShowDialog();
                msmqHelper.SendMessage("New Record " + this.FirstName + " " + this.LastName + "");
                logEventHelpers.LogEventMessageInfo("New Record:\nFirst Name: " + this.FirstName + "\nLast Name: " + this.LastName + "\nAddress: " + this.Address + "\nVaccine: " + this.VaccineName + "");
            }
            
        }

        #endregion Methods
    }
}