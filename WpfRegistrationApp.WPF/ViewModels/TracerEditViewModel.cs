﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;
using WpfRegistration.EntityFramework;
using WpfRegistration.EntityFramework.Services;
using WpfRegistrationApp.WPF.Commands;
using WpfRegistrationApp.WPF.State;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class TracerEditViewModel : BaseViewModel
    {
        private IServiceAgent _serviceAgent;
        IDataService<UserModel> dataService = new GenericDataService<UserModel>(new DbContextFactory());
        UserModel user = new UserModel();

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

        private CustomCommand _updateCommand;

        public TracerEditViewModel(IServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;

            SubmitCommand = new CustomCommand(this.Update);
        }

        public CustomCommand SubmitCommand
        { get; set; }

        public void Update(dynamic obj)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure about the information you have entered?", "WPF Tracer App", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        dataService.Update(this.Id, new UserModel() { FirstName = this.FirstName, LastName = this.LastName, Username = this.UserName, Email = this.Email, Address = this.Address, DateFirstDose = this.DateFirstDose, NumberofShots = this.NumberofShots, VaccineName = this.VaccineName, isBoosterShot = this.IsBooster, isVaccinated = this.IsVaccinated });
                        MessageBox.Show("Profile updated");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }

        }
        public void LoadUserById()
        {
            try
            {
                _serviceAgent.GetUserbyId((_users, error) => UserLoaded(_users, error));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UserLoaded(ObservableCollection<UserModel> users, Exception error)
        {
            if (error == null)
            {
                this.Users = users;
                NotifyError("Loaded", null);
                LoadUser();
            }
            else
            {
                NotifyError(error.Message, error);
            }
            // isbusy = false;
        }
        private void NotifyError(string message, Exception error)
        {
        }
        private void LoadUser()
        {
            List<UserModel> users = new List<UserModel>();
            if (Users != null)
            {
                users = Users.ToList();
                foreach (var item in users)
                {
                    this._firstName = item.FirstName;
                    this._lastName = item.LastName;
                    this._address = item.Address;
                    this._email = item.Email;
                    this._userName = item.Username;
                    this._dateFirstDose = item.DateFirstDose;
                    this._numberofShots = item.NumberofShots;
                    this.VaccineName = item.VaccineName;
                    this.IsBooster = item.isBoosterShot;
                    this.IsVaccinated = item.isVaccinated;
                }
            }
        }
    }
}
