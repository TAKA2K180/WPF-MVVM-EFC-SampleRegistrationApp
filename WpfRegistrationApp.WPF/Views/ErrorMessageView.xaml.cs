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
using System.Windows.Shapes;
using WpfRegistrationApp.WPF.ViewModels;

namespace WpfRegistrationApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for ModalWindows.xaml
    /// </summary>
    public partial class ModalWindows : Window
    {
        public ModalWindows()
        {
            InitializeComponent();
            ErrorViewModel errorViewModel = new ErrorViewModel();
            DataContext = errorViewModel;
        }

        private void btnOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
