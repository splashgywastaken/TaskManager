using System;
using System.Net;
using System.Security;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.Services.Misc;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;
using static System.Net.WebRequestMethods;

namespace TaskManagerWPF.ViewModel.Window
{
    public class AuthWindowViewModel : ViewModelBase
    {
        // Data fields

        // Fields
        private string _username = null!;
        private SecureString? _password;
        private string _errorMessage = null!;
        private bool _isViewVisible = true;
        private bool _isLoggingIn = false;

        // Properties
        public string Username
        {
            get => _username;
            set
            {
                _username = value ?? throw new ArgumentNullException(nameof(value)); 
                OnPropertyChanged();
            }
        }

        public SecureString? Password
        {
            get => _password;
            set
            {
                _password = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged();
            }
        }

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set
            {
                _isLoggingIn = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; } = null!;

        public AuthWindowViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            return true;
        }

        private void ExecuteSignUpCommand(object obj)
        {
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private async void ExecuteLoginCommand(object obj)
        {
            IsLoggingIn = true;

            // Getting httpClient to do requests on 
            var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            // Preparing data
            var password = SecureStringTools.SecureStringToString(Password!);
            var userLoginData = new UserLoginModel(Username, password);
            var route = "/user/login";
            // Doing a request
            var response = await httpClientService.PostAsync(userLoginData, route);

            if (response.IsSuccessStatusCode)
            {
                UserDataAccess.UserDataModel = await HttpClientService.DeserializeResponse<UserDataModel>(response);
                IsLoggingIn = false;
                IsViewVisible = false;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ErrorMessage = "* incorrect email or password";
                IsLoggingIn = false;
            }
        }
    }
}
