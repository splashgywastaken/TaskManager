using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        // Public fields
        private MainWindowViewModel MainWindowViewModel { get; set; }

        // Private fields
        private readonly UserDataModel _userData;

        // Property related fields
        private string _title = "Home page";
        private bool _isUserDataLoaded;
        private bool _isDeleteButtonVisible = true;
        private bool _isAcceptCancelButtonsVisible;

        public HomeViewModel()
        {
            var viewDataService = App.AppHost!.Services.GetRequiredService<ViewDataService>();
            MainWindowViewModel = viewDataService.GetView("MainViewModel") as MainWindowViewModel ?? throw new InvalidOperationException();

            _userData = App.AppHost!.Services.GetRequiredService<UserDataAccess>().UserDataModel;
            IsUserDataLoaded = true;
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
            CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);
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

        public bool IsDeleteButtonVisible
        {
            get => _isDeleteButtonVisible;
            set
            {
                _isDeleteButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsAcceptCancelButtonsVisible
        {
            get => _isAcceptCancelButtonsVisible;
            set
            {
                _isAcceptCancelButtonsVisible = value;
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
        public ICommand DeleteCommand { get; set; }
        public ICommand AcceptDeleteCommand { get; set; }
        public ICommand CancelDeleteCommand { get; set; }

        private bool CanExecuteCancelDeleteCommand(object obj)
        {
            return IsAcceptCancelButtonsVisible;
        }

        private void ExecuteCancelDeleteCommand(object obj)
        {
            IsDeleteButtonVisible = true;
            IsAcceptCancelButtonsVisible = false;
        }

        private bool CanExecuteAcceptDeleteCommand(object obj)
        {
            return IsAcceptCancelButtonsVisible;
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
            IsAcceptCancelButtonsVisible = false;
        }
        
        private bool CanExecuteDeleteCommand(object obj)
        {
            return IsDeleteButtonVisible;
        }

        private void ExecuteDeleteCommand(object obj)
        {
            IsDeleteButtonVisible = false;
            IsAcceptCancelButtonsVisible = true;
        }

        private bool CanExecuteEditCommand(object obj)
        {
            return true;
        }

        private void ExecuteEditCommand(object obj)
        {
            
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
