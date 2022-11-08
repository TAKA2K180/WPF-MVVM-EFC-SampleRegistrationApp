using System;
using System.Collections.Generic;
using System.Text;
using WpfRegistrationApp.WPF.State.Navigators;

namespace WpfRegistrationApp.WPF.ViewModels.Factories
{
    public interface IRootSimpleTraderViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}
