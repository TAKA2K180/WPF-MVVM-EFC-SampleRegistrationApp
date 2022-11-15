using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WpfRegistrationApp.WPF.Commands;
using WpfRegistrationApp.WPF.Models;
using WpfRegistrationApp.WPF.ViewModels;

namespace WpfRegistrationApp.WPF.State.Navigators
{
    public class Navigator : ObservableObjects, INavigator
    {
        private BaseViewModel _currentViewmodel;
        private TracerEditViewModel _tracerEdit;
        public BaseViewModel currentViewmodel 
        {
            get
            {
                return _currentViewmodel;
            }
            set
            {
                _currentViewmodel = value;
                OnPropertyChanged(nameof(currentViewmodel));
            }
        }

        public ICommand UpdateCurrentViewModel => new UpdateCurrentViewModelCommand(this);
    }
}
