using System;
using System.Configuration;
using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.View;
using TaskManagerWPF.View.Windows;

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
                    services.AddSingleton<UserDataAccess>();
                    services.AddTransient<HttpClient>();
                }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<AuthWindow>();
            startupForm.Show();
            startupForm.IsVisibleChanged += (s, e) =>
            {
                if (startupForm is {IsVisible: false, IsLoaded: true})
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                }
            };

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();

            base.OnExit(e);
        }
    }
}
