using DiveLog.Commands;
using DiveLog.Core;
using DiveLog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiveLog.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public string WelcomeMessage { get; set; }

        public DateTime CurrentDatetime { get; set; }

        public INavigationService _navigation;
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(INavigationService navService)
        {
            WelcomeMessage = $"Hello {Environment.UserName}.";
            CurrentDatetime = DateTime.Now;

            Navigation = navService;

            WireCommands();
        }

        public RelayCommand NavigateHomepageCommand { get; set; }
        public RelayCommand NavigateUserProfileCommand { get; set; }
        public RelayCommand NavigateDiveLogsCommand { get; set; }

        public void WireCommands()
        {
            NavigateHomepageCommand = new RelayCommand(
                param =>
                {
                    Navigation.NavigateTo<HomeViewModel>();
                },
                param => true);

            NavigateUserProfileCommand = new RelayCommand(
                param =>
                {
                    Navigation.NavigateTo<UserProfileViewModel>();
                },
                param => true);

            NavigateDiveLogsCommand = new RelayCommand(
                param =>
                {
                    Navigation.NavigateTo<DiveLogsViewModel>();
                },
                param => true);
        }

    }
}
