﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfRegistration.EntityFramework.Services;
using WpfRegistrationApp.WPF.State.Navigators;
using WpfRegistrationApp.WPF.ViewModels;

namespace WpfRegistrationApp.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        #region Variable declarations
        public event EventHandler CanExecuteChanged;
        private readonly INavigator _navigator;
        IServiceAgent sa = new ServiceAgent();
        #endregion

        #region Constructors
        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                if (parameter != null)
                {
                    ViewType viewType = (ViewType)parameter;
                    switch (viewType)
                    {
                        case ViewType.Home:
                            _navigator.currentViewmodel = new HomeViewModel();
                            break;
                        case ViewType.Profile:
                            _navigator.currentViewmodel = new ProfileViewModel();
                            break;
                        case ViewType.TracerInfo:
                            _navigator.currentViewmodel = new TracerViewModel(sa);
                            break;
                        case ViewType.TracerEdit:
                            _navigator.currentViewmodel = new TracerEditViewModel(sa);
                            break;
                    }
                }
                else
                {
                    throw new ArgumentException(nameof(parameter), "View not found");
                }

            }
        }
        #endregion
    }
}