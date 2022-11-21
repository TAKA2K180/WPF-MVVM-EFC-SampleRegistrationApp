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
using WpfRegistration.EntityFramework.Services;
using WpfRegistrationApp.WPF.Enums;
using WpfRegistrationApp.WPF.ViewModels;

namespace WpfRegistrationApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for TracerEditView.xaml
    /// </summary>
    public partial class TracerEditView : UserControl
    {
        private readonly TracerEditViewModel viewModel;
        IServiceAgent sa = new ServiceAgent();
        int numberOfVaccine = 0;
        bool isVaccinated;
        bool isBooster;
        public TracerEditView()
        {
            InitializeComponent();
            viewModel = new TracerEditViewModel();
            DataContext = this.viewModel;

            cbVaccineName.ItemsSource = Enum.GetValues(typeof(VaccineNames.Vaccines));
            NumberOfVaccine();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void chkVaccinated_Checked(object sender, RoutedEventArgs e)
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

        private void cbNumberofVaccines_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //cbNumberofVaccines.Items.Clear();
            
        }

        void window_Closing(object sender, global::System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            viewModel.OnExit();
        }
    }
}
