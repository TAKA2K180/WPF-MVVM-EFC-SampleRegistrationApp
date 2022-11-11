using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WpfRegistrationApp.WPF.State.Helpers
{
    public class LogEventHelpers
    {
        public void LogEventMessageInfo(string msg)
        {
            EventLog log = new EventLog();
            log.Source = "WPF event logs";
            log.WriteEntry(msg, EventLogEntryType.Information);
        }

        public void LogEventMessageError(string msg)
        {
            EventLog log = new EventLog();
            log.Source = "WPF event logs";
            log.WriteEntry(msg, EventLogEntryType.Error);
        }
        public void LogEventMessageWarning(string msg)
        {
            EventLog log = new EventLog();
            log.Source = "WPF event logs";
            log.WriteEntry(msg, EventLogEntryType.Warning);
        }
    }
}
