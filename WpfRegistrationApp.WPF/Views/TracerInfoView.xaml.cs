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
using System.Linq;
using System.Threading.Tasks;
using WpfRegistrationApp.WPF.ViewModels;
using MaterialDesignThemes.Wpf;

namespace WpfRegistrationApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for TracerInfoView.xaml
    /// </summary>
    public partial class TracerInfoView : UserControl
    {
        private readonly TracerViewModel _vm; 
        IServiceAgent sa = new ServiceAgent();
        public TracerInfoView()
        {
            InitializeComponent();
            
            _vm = new TracerViewModel(sa);
            DataContext = this._vm;

           // _vm.LoadUsers();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LvUserlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object parameter = default;
            var item = (ListView)sender;
            TracerViewModel tracer = new TracerViewModel(sa);
            tracer.SelectedItem((UserModel)item.SelectedItem);
        }
    }
}
