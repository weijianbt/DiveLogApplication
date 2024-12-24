using DiveLog.ViewModels;
using DiveLog.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DiveLog.Core;
using DiveLog.Services;
using DiveLog.Models;

namespace DiveLog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(
            provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            });
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<UserProfileViewModel>();
            services.AddSingleton<DiveLogsViewModel>();
            services.AddSingleton<AddLicenseViewModel>();
            services.AddSingleton<INavigationService,  NavigationService>();
            services.AddSingleton<LicensesManager>();
            services.AddSingleton<Func<Type, ViewModel>>(ServiceProvider => viewModelType => (ViewModel)ServiceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
