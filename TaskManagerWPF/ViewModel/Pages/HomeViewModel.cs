using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.View.Windows;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.Pages
{
    public class HomeViewModel : ViewModelBase
    {
        // Public fields
        private MainWindowViewModel MainWindowViewModel { get; set; }

        // Private fields
        private readonly UserDataModel _userData;
        private UserWithAllData _oldUser = null!;

        // Property related fields
        private string _title = "Home page";
        private bool _isUserDataLoaded;
        private bool _isEditButtonVisible = true;
        private bool _isAcceptCancelEditButtonsVisible;
        private bool _isDeleteButtonVisible = true;
        private bool _isAcceptCancelDeleteButtonsVisible;
        private bool _isUserAdmin = true;

        public HomeViewModel()
        {
            _isUserAdmin = UserDataAccess.UserDataModel.UserRole == "admin";
            var viewDataService = App.AppHost!.Services.GetRequiredService<ViewDataService>();
            MainWindowViewModel = viewDataService.GetView("MainViewModel") as MainWindowViewModel ?? throw new InvalidOperationException();

            _userData = UserDataAccess.UserDataModel;
            IsUserDataLoaded = true;
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            AcceptEditCommand = new ViewModelCommand(ExecuteAcceptEditCommand, CanExecuteAcceptEditCommand);
            CancelEditCommand = new ViewModelCommand(ExecuteCancelEditCommand, CanExecuteCancelEditCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
            CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);
            OpenAdminPanelCommand = new ViewModelCommand(ExecuteOpenAdminPanelCommand, CanExecuteOpenAdminPanelCommand);
        }
        
        private bool CanExecuteOpenAdminPanelCommand(object obj)
        {
            return IsUserAdmin;
        }

        private void ExecuteOpenAdminPanelCommand(object obj)
        {
            var adminPanel = new AdminPanelWindow();
            adminPanel.Show();
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public bool IsUserDataLoaded
        {
            get => _isUserDataLoaded;
            set
            {
                _isUserDataLoaded = value;
                OnPropertyChanged();
            }
        }
        public bool IsEditButtonVisible
        {
            get => _isEditButtonVisible;
            set
            {
                _isEditButtonVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsAcceptCancelEditButtonsVisible
        {
            get => _isAcceptCancelEditButtonsVisible;
            set
            {
                _isAcceptCancelEditButtonsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsDeleteButtonVisible
        {
            get => _isDeleteButtonVisible;
            set
            {
                _isDeleteButtonVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsAcceptCancelDeleteButtonsVisible
        {
            get => _isAcceptCancelDeleteButtonsVisible;
            set
            {
                _isAcceptCancelDeleteButtonsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool IsUserAdmin
        {
            get => _isUserAdmin;
            set
            {
                _isUserAdmin = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get => _userData.UserName;
            set
            {
                _userData.UserName = value;
                OnPropertyChanged();
            }
        }
        public string UserEmail
        {
            get => _userData.UserEmail;
            set
            {
                _userData.UserEmail = value;
                OnPropertyChanged();
            }
        }
        public int UserAchievementsScore
        {
            get => _userData.UserAchievementsScore;
            set
            {
                _userData.UserAchievementsScore = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand EditCommand { get; set; }
        public ICommand AcceptEditCommand { get; set; }
        public ICommand CancelEditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AcceptDeleteCommand { get; set; }
        public ICommand CancelDeleteCommand { get; set; }
        public ICommand OpenAdminPanelCommand { get; set; }

        private bool CanExecuteCancelDeleteCommand(object obj)
        {
            return IsAcceptCancelDeleteButtonsVisible;
        }

        private void ExecuteCancelDeleteCommand(object obj)
        {
            IsDeleteButtonVisible = true;
            IsAcceptCancelDeleteButtonsVisible = false;
        }

        private bool CanExecuteAcceptDeleteCommand(object obj)
        {
            return IsAcceptCancelDeleteButtonsVisible;
        }

        private async void ExecuteAcceptDeleteCommand(object obj)
        {
            var httpClient = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var url = $"/user/{_userData.UserId}";

            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                MainWindowViewModel!.Logout();
            }

            IsDeleteButtonVisible = true;
            IsAcceptCancelDeleteButtonsVisible = false;
        }
        
        private bool CanExecuteDeleteCommand(object obj)
        {
            return IsDeleteButtonVisible;
        }

        private void ExecuteDeleteCommand(object obj)
        {
            IsDeleteButtonVisible = false;
            IsAcceptCancelDeleteButtonsVisible = true;
        }

        private bool CanExecuteEditCommand(object obj)
        {
            return IsEditButtonVisible;
        }

        private void ExecuteEditCommand(object obj)
        {
            _oldUser = new UserWithAllData(UserName);

            IsEditButtonVisible = false;
            IsAcceptCancelEditButtonsVisible = true;
        }
        private void ExecuteCancelEditCommand(object obj)
        {
            UserName = _oldUser.UserName;

            IsEditButtonVisible = true;
            IsAcceptCancelEditButtonsVisible= false;
        }
        private bool CanExecuteCancelEditCommand(object obj)
        {
            return _isAcceptCancelEditButtonsVisible;
        }

        private void ExecuteAcceptEditCommand(object obj)
        {
            // Do smth with data

            IsEditButtonVisible = true;
            IsAcceptCancelEditButtonsVisible = false;
        }

        private bool CanExecuteAcceptEditCommand(object obj)
        {
            return _isAcceptCancelEditButtonsVisible;
        }
        
        private async void UpdateUserData()
        {
            var httpClient = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = $"/user/{_userData.UserId}";
                
            var response = await httpClient.PutAsync(_userData, route);

#if DEBUG
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                // Updated successfully
            }
            else
            {
                MessageBox.Show("Error occurred while updating user"); 
#endif
            }
        }
    }
}
