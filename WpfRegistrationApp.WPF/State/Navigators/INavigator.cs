using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfRegistrationApp.WPF.ViewModels;

namespace WpfRegistrationApp.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Profile,
        TracerInfo,
        TracerEdit
    }
    public interface INavigator
    {
        BaseViewModel currentViewmodel { get; set; }
        ICommand UpdateCurrentViewModel { get; }

    }
}
