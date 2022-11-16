using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfRegistrationApp.WPF.Commands;
using WpfRegistrationApp.WPF.State.Helpers;
using WpfRegistrationApp.WPF.Views;

namespace WpfRegistrationApp.WPF.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
		private string _messageBody;

		public string MessageBody
		{
			get { return _messageBody; }
			set { _messageBody = value; OnPropertyChanged(MessageBody); }
		}

		public CustomCommand ClickYes { get; set; }
		public CustomCommand ClickNo { get; set; }

		public bool isYesClicked { get; set; }
		public bool isNoClicked { get; set; }

		public MessageViewModel()
		{
			this.ClickYes = new CustomCommand(OnClickYes);
			this.ClickNo = new CustomCommand(OnClickNo);
			_messageBody = MessageHelper.messageBody;
		}

		public void OnClickYes(dynamic obj)
		{
			MessageHelper.isYesClicked = true;
        }

		public void OnClickNo(dynamic obj)
		{
			MessageHelper.isNoClicked = true;
            MessageHelper.isYesClicked = false;
        }

        private void CloseWindow(MessageBoxView window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
