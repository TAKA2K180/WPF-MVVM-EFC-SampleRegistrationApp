using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRegistrationApp.WPF.ViewModels.Factories
{
    public interface ISimpleTraderViewModelFactory<T> where T : BaseViewModel
    {
        T CreateViewModel();
    }
}
