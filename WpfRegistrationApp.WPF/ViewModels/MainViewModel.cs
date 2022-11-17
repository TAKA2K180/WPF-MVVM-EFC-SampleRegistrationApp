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

        private bool _isClosing = true;

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

        #endregion Properties

        #region Methods

        public void OnExit()
        {
            if (navigator.currentViewmodel != this)
            {
                IsClosing = false;
            }
        }

        #endregion Methods
    }
}