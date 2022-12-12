using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Newtonsoft.Json;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.Services.Misc;
using TaskManagerWPF.ViewModel.Base;
using static System.Net.WebRequestMethods;

namespace TaskManagerWPF.ViewModel
{
    public class AuthWindowViewModel : ViewModelBase
    {
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
        public ICommand ShowPasswordCommand { get; set; } = null!;

        public AuthWindowViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
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
            var userDataAccess = App.AppHost!.Services.GetService(typeof(UserDataAccess)) as UserDataAccess;

            // Getting httpClient to do requests on 
            using var httpClient = App.AppHost.Services.GetService(typeof(HttpClient)) as HttpClient;
            // Preparing data
            var password = SecureStringTools.SecureStringToString(Password!);
            var userLoginData = new UserLoginModel(Username, password);
            var json = JsonConvert.SerializeObject(userLoginData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            // Doing a request
            var url = "https://localhost:7217/user/login";
            var response = await httpClient!.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = "Logged in";
                url = "https://localhost:7217/user/";
                var responseString = await response.Content.ReadAsStringAsync();
                userDataAccess!.UserDataModel = JsonConvert.DeserializeObject<UserDataModel>(responseString)!;
                IsLoggingIn= false;
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
