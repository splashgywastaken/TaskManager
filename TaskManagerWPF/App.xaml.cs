using System;
using System.Configuration;
using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.View;
using TaskManagerWPF.View.Windows;
using TaskManagerWPF.ViewModel;

namespace TaskManagerWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<AuthWindow>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<SignUpWindow>();
                    services.AddSingleton<UserDataAccess>();
                    services.AddSingleton<ViewDataService>();
                    services.AddTransient(_ =>
                    {
                        const string domainUrl = "https://localhost:7217";
                        //const string domainUrl = "https://localhost:44371";

                        return new HttpClientService(domainUrl);
                    });
                }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            
            var authWindow = new AuthWindow();
            authWindow.Show();

            authWindow.IsVisibleChanged += (s, e) =>
            {
                if (authWindow is {IsVisible: false, IsLoaded: true})
                {
                    var mainWindow = new MainWindow();
                    var mainWindowViewModel = new MainWindowViewModel(authWindow);
                    mainWindow.DataContext = mainWindowViewModel;
                    mainWindow.Show();
                    authWindow.Hide();
                }
            };

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            var httpClient = AppHost!.Services.GetRequiredService<HttpClientService>();
            await httpClient.PostAsync("/user/logout");

            await AppHost!.StopAsync();

            base.OnExit(e);
        }
    }
}
