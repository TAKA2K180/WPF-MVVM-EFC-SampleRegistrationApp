using System.Windows;
using WpfRegistrationApp.WPF.State.Navigators;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Constructor

        public MainViewModel()
        {
        }

        #endregion Constructor

        #region Properties

        public INavigator navigator { get; set; } = new Navigator();

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}