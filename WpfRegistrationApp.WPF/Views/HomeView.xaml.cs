using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfRegistration.Domain.Models;
using WpfRegistration.Domain.Services;
using WpfRegistration.EntityFramework.Services;
using WpfRegistration.EntityFramework;
using WpfRegistrationApp.WPF.Enums;
using System.Threading.Tasks;
using WpfRegistrationApp.WPF.ViewModels;

namespace WpfRegistrationApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        #region Variable declarations
        int numberOfVaccine = 0;
        bool isVaccinated = false;
        bool isBooster = false;
        IDataService<UserModel> dataService = new GenericDataService<UserModel>(new DbContextFactory());

        #endregion

        #region Constructor
        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
            cbVaccineName.ItemsSource = Enum.GetValues(typeof(VaccineNames.Vaccines));
            NumberOfVaccine();
            dtpFirstDose.SelectedDate = DateTime.Now;
        }
        #endregion

        #region Event Handlers
        private void HomeView_Load(Object sender, EventArgs e)
        {
            cbVaccineName.ItemsSource = Enum.GetValues(typeof(VaccineNames.Vaccines));
        }

        private void chkVaccinated_CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            if (chkVaccinated.IsChecked != true)
            {
                chkBooster.IsEnabled = false;
                cbVaccineName.IsEnabled = false;
                cbNumberofVaccines.IsEnabled = false;
                isVaccinated = true;
            }
            else
            {
                chkBooster.IsEnabled = true;
                cbVaccineName.IsEnabled = true;
                cbNumberofVaccines.IsEnabled = true;
            }
        }
        private void NumberOfVaccine()
        {
            for (int i = 1; i < 5; i++)
            {
                numberOfVaccine = i;
                cbNumberofVaccines.Items.Add(numberOfVaccine);
            }
        }

        #endregion

        private void chkBooster_Checked(object sender, RoutedEventArgs e)
        {
            if (chkBooster.IsChecked == true)
            {
                isBooster = true;
            }
            else
            {
                isBooster = false;
            }
        }
    }
}
