using System;
using System.Collections.Generic;
using System.Text;
using WpfRegistrationApp.WPF.State.Navigators;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator navigator { get; set; } = new Navigator();
    }
}
