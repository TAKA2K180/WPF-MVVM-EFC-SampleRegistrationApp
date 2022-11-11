using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfRegistrationApp.WPF.State.Helpers;
using WpfRegistrationApp.WPF.ViewModels;

namespace WpfRegistrationApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            LogEventHelpers logEventHelpers = new LogEventHelpers();
            logEventHelpers.LogEventMessageError(e.Exception.Message);
            e.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Window window = new MainWindow();
            window.DataContext = new MainViewModel();
            window.Show();
            //base.OnStartup(e);
        }
    }
}
