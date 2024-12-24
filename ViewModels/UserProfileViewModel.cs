using DiveLog.Commands;
using DiveLog.Core;
using System;
using DiveLog.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using DiveLog.Views;
using System.ComponentModel;

namespace DiveLog.ViewModels
{
    public class UserProfileViewModel : ViewModel
    {
        private string _profilePicturePath;
        private DateTime _diverSinceDate;
        private string _diverSinceText;
        private readonly LicensesManager _licensesManager;

        public UserProfileViewModel(LicensesManager licensesManager)
        {
            _licensesManager = licensesManager;
            _licensesManager.LicenseAdded += OnLicenseAdded;
            ProfilePicturePath = @"C:\Users\twjbr\Documents\diving\diving trip - Tenggol August 2024\IMG_5281.JPG";
            WireCommands();
        }

        public string ProfilePicturePath
        {
            get => _profilePicturePath;
            set
            {
                _profilePicturePath = value;
                OnPropertyChanged();
            }
        }

        public DateTime DiverSinceDate
        {
            get => _diverSinceDate;
            set
            {
                _diverSinceDate = value;
                OnPropertyChanged();
            }
        }

        public string DiverSinceText 
        { 
            get => _diverSinceText;
            set
            {
                _diverSinceText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Licenses> DiveLicenses => _licensesManager.Licenses;

        public RelayCommand SelectProfilePictureCommand { get; set; }

        public RelayCommand AddNewLicenseCommand {  get; private set; }
        

        private void WireCommands()
        {
            SelectProfilePictureCommand = new RelayCommand(
                param =>
                {
                    LoadDirectory();
                }, param=> true);

            AddNewLicenseCommand = new RelayCommand(
               param =>
               {
                   var dialog = new AddLicenseView
                   {
                       DataContext = new AddLicenseViewModel(_licensesManager)
                   };
                   dialog.ShowDialog();
               }, param => true);

        }

        private void OnLicenseAdded()
        {
            DiverSinceDate = _licensesManager.GetEarliestDate();
            DiverSinceText = _licensesManager.ConvertDateToText(DiverSinceDate);
        }

        private void LoadDirectory()
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp;) | *.jpg;*.jpeg;*.png;*.bmp;"
            };
            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                ProfilePicturePath = fileDialog.FileName;
            }
        }
    }
}
