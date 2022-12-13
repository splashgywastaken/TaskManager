using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.Misc;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.View.Windows;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel
{
    class SignUpWindowViewModel : ViewModelBase
    {
        // Properties
        public AuthWindowViewModel? ParentViewModel { get; set; }

        // View data 
        private string _errorMessage = null!;
        private string _successMessage = null!;
        private bool _isSigningUp;
        private bool _isSignedUp;
        private bool _isViewVisible;
        
        // User data
        private string _userName = null!;
        private string _email = null!;
        private SecureString? _password = null!;
        private SecureString? _confirmPassword = null!;
        
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public SecureString? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public SecureString? ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public string SuccessMessage
        {
            get => _successMessage;
            set
            {
                _successMessage = value;
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
        public bool IsSigningUp
        {
            get => _isSigningUp;
            set
            {
                _isSigningUp = value;
                OnPropertyChanged();
            }
        }
        public bool IsSignedUp
        {
            get => _isSignedUp;
            set
            {
                _isSignedUp = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand SignUpCommand { get; set; }

        public SignUpWindowViewModel()
        {
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Email) || Email.Length < 3
                                                 || Password == null
                                                 || Password.Length < 3 
                                                 || ConfirmPassword == null
                                                 || ConfirmPassword.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private async void ExecuteSignUpCommand(object obj)
        {
            IsSigningUp = true;
            string password = SecureStringTools.SecureStringToString(Password!);
            string confirmPassword = SecureStringTools.SecureStringToString(ConfirmPassword!);

            if (password != confirmPassword)
            {
                ErrorMessage = "* Unable to sign up, incorrect credentials";
                return;
            }

            var httpClient = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var user = new UserSignUpModel(UserName, Email, password);
            var route = "/user/register";

            var response = await httpClient.PostAsync(user, route);

            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "Signed up successfully, you can close this window";
                IsSignedUp = true;
                ErrorMessage = "";
                ParentViewModel!.Username = Email;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ErrorMessage = "Email is not unique";
            }
            IsSigningUp = false;
        }
    }
}
