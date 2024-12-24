using DiveLog.Commands;
using DiveLog.Core;
using DiveLog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace DiveLog.ViewModels
{
    public class AddLicenseViewModel : ViewModel
    {
        private readonly LicensesManager _licensesManager;
        
        private string _licenseLevel;
        private string _licenseProvider;
        private string _diveCentre;
        private string _location;
        private DateTime _issuedDate;
        private readonly DateTime _currentDate;
        private string _id;
        private bool _canSave;
        private Licenses newLicenseData;

        public AddLicenseViewModel(LicensesManager licensesManager)
        { 
            _licensesManager = licensesManager;
            IssuedDate = DateTime.Now;
            _currentDate = DateTime.Now;
            WireCommands();
        }

        public string LicenseLevel
        {
            get => _licenseLevel;
            set
            {
                _licenseLevel = value;
                OnPropertyChanged();
                ValidateCanSave();
            }
        }

        public string LicenseProvider
        {
            get => _licenseProvider;
            set
            {
                _licenseProvider = value;
                OnPropertyChanged();
                ValidateCanSave();
            }
        }

        public string DiveCentre
        {
            get => _diveCentre;
            set
            {
                _diveCentre = value;
                OnPropertyChanged();
                ValidateCanSave();
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
                ValidateCanSave();
            }
        }

        public DateTime IssuedDate
        {
            get => _issuedDate;
            set
            {
                _issuedDate = value;
                OnPropertyChanged();
                ValidateCanSave();
            }
        }

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
                ValidateCanSave();
            }
        }

        public bool CanSave
        {
            get => _canSave;
            set
            {
                _canSave = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }

        public RelayCommand CancelCommand {  get; set; }

        public RelayCommand ClearCommand {  get; set; }

        private void WireCommands()
        {
            SaveCommand = new RelayCommand(
                param =>
                {
                    newLicenseData = new Licenses
                    {
                        LicenseLevel = LicenseLevel,
                        LicenseProvider = LicenseProvider,
                        DiveCentre = DiveCentre,
                        Location = Location,
                        IssuedDate = IssuedDate,
                        Id = Id
                    };

                    _licensesManager.AddLicense(newLicenseData);
                },
                param => CanSave);

            CancelCommand = new RelayCommand(
                param =>
                {
                    if (param is Window window)
                    {
                        window.Close();
                    }
                },
                param => true);

            ClearCommand = new RelayCommand(
                param =>
                {
                    LicenseLevel = string.Empty;
                    LicenseProvider = string.Empty;
                    DiveCentre = string.Empty;
                    Location = string.Empty;
                    IssuedDate = _currentDate;
                    Id = string.Empty;

                    CanSave = false;
                },
                param => true);
        }
    
        private void ValidateCanSave()
        {
            var validations = new List<Func<bool>>
            {
                () => !string.IsNullOrEmpty(LicenseLevel),
                () => !string.IsNullOrEmpty(LicenseProvider),
                () => !string.IsNullOrEmpty(DiveCentre),
                () => !string.IsNullOrEmpty(Location),
                () => !string.IsNullOrEmpty(Id)
            };

            CanSave = validations.TrueForAll(validate => validate());

        }
    }
}
