using System;
using System.Collections.Generic;
using System.Text;
using WpfRegistrationApp.WPF.Commands;
using WpfRegistrationApp.WPF.State.Helpers;
using WpfRegistrationApp.WPF.Views;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {
		#region Constructor
		public ErrorViewModel()
		{
			_errorMessage = ExceptionHelper.exceptionMessage;
			this.ClickCommand = new CustomCommand(OnClick);
		} 
		#endregion

		#region Properties
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
		}
		public CustomCommand ClickCommand { get; set; }
		#endregion

		#region Methods
		public void OnClick(dynamic obj)
		{
			//ModalWindows modalWindows = new ModalWindows();
			//modalWindows.Close();
		}
		#endregion
	}
}
