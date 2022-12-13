using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.View.Windows;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Private fields
        private AuthWindow? AuthWindow { get; set; }

        // Property related private fields
        private bool _isViewVisible = true;

        // Properties
        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand LogoutCommand { get; set; }

        public MainWindowViewModel()
        {
            AuthWindow = null;
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand, CanExecuteLogoutCommand);
        }

        public MainWindowViewModel(AuthWindow authWindow)
        {
            AuthWindow = authWindow;
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand, CanExecuteLogoutCommand);
        }

        private bool CanExecuteLogoutCommand(object obj)
        {
            return true;
        }

        private async void ExecuteLogoutCommand(object obj)
        {
            var httpClient = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = "/user/logout";
            var response = await httpClient!.PostAsync(route);
            IsViewVisible = false;
            AuthWindow?.Show();
        }
    }
}
