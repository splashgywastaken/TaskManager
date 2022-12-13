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
                    services.AddTransient(_ =>
                    {
                        const string domainUrl = "https://localhost:7217";

                        return new HttpClientService(domainUrl);
                    });
                }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            //var authWindow = AppHost.Services.GetRequiredService<AuthWindow>();
            //var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            var authWindow = new AuthWindow();
            authWindow.Show();

            // TODO: Think about how to make this right

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
