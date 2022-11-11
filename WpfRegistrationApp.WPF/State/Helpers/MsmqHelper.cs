using MSMQ.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRegistrationApp.WPF.State.Helpers
{
    public class MsmqHelper
    {
        public void SendMessage(string body)
        {
            Message msg = new Message();
            msg.Body = body;
            MessageQueue messageQueue = new MessageQueue(".\\Private$\\WpfRegistration");
            messageQueue.Label = "WPF Registration Message";
            messageQueue.Send(msg);
        }
    }
}
