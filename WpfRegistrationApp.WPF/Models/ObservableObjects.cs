using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfRegistrationApp.WPF.Models
{
    public class ObservableObjects : INotifyPropertyChanged
    {
        #region Variable declarations
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
