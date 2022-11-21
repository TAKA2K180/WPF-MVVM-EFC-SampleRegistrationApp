using System.Windows;
using Microsoft.Extensions.Hosting;
using WpfRegistrationApp.WPF.State.Helpers;
using WpfRegistrationApp.WPF.ViewModels;
using WpfRegistrationApp.WPF.Views;

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

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            LogEventHelpers logEventHelpers = new LogEventHelpers();
            logEventHelpers.LogEventMessageError(e.Exception.Message);
            ExceptionHelper.exceptionMessage = $"{e.Exception.Message} No changes were made";
            ModalWindows modalWindows = new ModalWindows();
            modalWindows.ShowDialog();
            e.Handled = true;
            ExceptionHelper.isExceptionHandled = true;
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