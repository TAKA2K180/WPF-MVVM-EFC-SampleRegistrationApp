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
    /// Interaction logic for MessageBoxView.xaml
    /// </summary>
    public partial class MessageBoxView : Window
    {
        public MessageBoxView()
        {
            InitializeComponent();
            MessageViewModel messageViewModel = new MessageViewModel();
            DataContext = messageViewModel;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
