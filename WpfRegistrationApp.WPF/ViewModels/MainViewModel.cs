using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using WpfRegistrationApp.WPF.State.Navigators;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator navigator { get; set; } = new Navigator();

        private bool _isClosing = true;

        public MainViewModel()
        {

        }

        public bool IsClosing
        {
            get { return _isClosing; }
            set { _isClosing = value; OnPropertyChanged("IsClosing"); OnPropertyChanged("textBlockVis"); }
        }
        public Visibility textBlockVis
        {
            get
            {
                return _isClosing ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public void OnExit()
        {
            if (navigator.currentViewmodel != this)
            {
                IsClosing = false;
            }
        }
    }
}
