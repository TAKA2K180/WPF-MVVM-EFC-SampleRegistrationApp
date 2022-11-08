using System;
using System.Collections.Generic;
using System.Text;
using WpfRegistrationApp.WPF.State.Navigators;

namespace WpfRegistrationApp.WPF.ViewModels.Factories
{
    public class RootSimpleTraderViewModelFactory : IRootSimpleTraderViewModelFactory
    {
        private readonly ISimpleTraderViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<ProfileViewModel> _portfolioViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<TracerViewModel> _tracerViewModelFactory;

        public RootSimpleTraderViewModelFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory, ISimpleTraderViewModelFactory<ProfileViewModel> portfolioViewModelFactory)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _portfolioViewModelFactory = portfolioViewModelFactory;
        }

        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _homeViewModelFactory.CreateViewModel();
                case ViewType.Profile:
                    return _portfolioViewModelFactory.CreateViewModel();
                case ViewType.TracerInfo:
                    return _tracerViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
