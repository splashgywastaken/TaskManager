using System;
using System.Security;
using System.Windows.Input;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel
{
    public class AuthWindowViewModel : ViewModelBase
    {
        // Fields
        private string _username;
        private SecureString? _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

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

        // Commands
        public ICommand LoginCommand { get; }
        public ICommand ShowPasswordCommand { get; }

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

        private void ExecuteLoginCommand(object obj)
        {
            // Implement how login will work here
        }
    }
}
