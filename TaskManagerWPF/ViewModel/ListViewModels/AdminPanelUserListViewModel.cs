using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Printing;
using System.Security.RightsManagement;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model.Enums;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class AdminPanelUserListViewModel : ViewModelBase
{
    private UsersListViewModel _parentViewModel;
    private UserWithAllData _oldUser = null!;

    #region DataProperties
    private int _userId;
    private string _userName;
    private string _email;
    private int _achievementsScore;
    private string _role;
    private ObservableCollection<string> _userRoles = null!;
    private bool _areAcceptCancelEditButtonsVisible;
    private bool _isEditButtonVisible = true;
    public int UserId
    {
        get => _userId;
        set => SetField(ref _userId, value);
    }
    public string UserName
    {
        get => _userName;
        set => SetField(ref _userName, value);
    }
    public string Email
    {
        get => _email;
        set => SetField(ref _email, value);
    }
    public int AchievementsScore
    {
        get => _achievementsScore;
        set => SetField(ref _achievementsScore, value);
    }
    public string Role
    {
        get => _role;
        set => SetField(ref _role, value);
    }
    public bool AreAcceptCancelEditButtonsVisible
    {
        get => _areAcceptCancelEditButtonsVisible;
        set => SetField(ref _areAcceptCancelEditButtonsVisible, value);
    }
    public bool IsEditButtonVisible
    {
        get => _isEditButtonVisible;
        set => SetField(ref _isEditButtonVisible, value);
    }
    public ObservableCollection<string> UserRoles
    {
        get => _userRoles;
        set => SetField(ref _userRoles, value);
    }
    #endregion
    #region UIProperties
    private bool _isDeleteButtonVisible = true;
    private bool _areAcceptCancelDeleteButtonsVisible;
    public bool IsDeleteButtonVisible
    {
        get => _isDeleteButtonVisible;
        set
        {
            _isDeleteButtonVisible = value;
            OnPropertyChanged();
        }
    }
    public bool AreAcceptCancelDeleteButtonsVisible
    {
        get => _areAcceptCancelDeleteButtonsVisible;
        set
        {
            _areAcceptCancelDeleteButtonsVisible = value;
            OnPropertyChanged();
        }
    }
    #endregion
    #region Commands
    public ICommand DeleteUser { get; }
    public ICommand AcceptDeleteUser { get; }
    public ICommand CancelDeleteUser { get; }
    public ICommand EditCommand { get; }
    public ICommand AcceptEditCommand { get; }
    public ICommand CancelEditCommand { get; }
    #endregion
    #region CommandsMethods
    private bool CanExecuteCancelDeleteUser(object obj)
    {
        return AreAcceptCancelDeleteButtonsVisible;
    }
    private void ExecuteCancelDeleteUser(object obj)
    {
        AreAcceptCancelDeleteButtonsVisible = false;
        IsDeleteButtonVisible = true;
    }
    private bool CanExecuteAcceptDeleteUser(object obj)
    {
        return AreAcceptCancelDeleteButtonsVisible;
    }
    private async void ExecuteAcceptDeleteUser(object obj)
    {
        var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
        var route = $"/user/{(int) obj}";

        var response = await httpClientService.DeleteAsync(route);

        if (!response.IsSuccessStatusCode)
        {
            MessageBox.Show($"Couldn't delete user {UserId}");
        }

        _parentViewModel.DeleteUserById((int) obj);
        AreAcceptCancelDeleteButtonsVisible = false;
        IsDeleteButtonVisible = true;
    }
    private bool CanExecuteDeleteUser(object obj)
    {
        return IsDeleteButtonVisible;
    }
    private void ExecuteDeleteUser(object obj)
    {
        AreAcceptCancelDeleteButtonsVisible = true;
        IsDeleteButtonVisible = false;
    }
    #endregion

    public AdminPanelUserListViewModel(UsersListViewModel parentViewModel, UserWithAllData user)
    {
        UserRoles = new ObservableCollection<string> { "user", "admin" };

        _parentViewModel = parentViewModel;

        _userId = user.UserId;
        _userName = new string(user.UserName);
        _email = new string(user.UserEmail);
        _achievementsScore = AchievementsScore;
        _role = user.UserRole;

        DeleteUser = new ViewModelCommand(ExecuteDeleteUser, CanExecuteDeleteUser);
        AcceptDeleteUser = new ViewModelCommand(ExecuteAcceptDeleteUser, CanExecuteAcceptDeleteUser);
        CancelDeleteUser = new ViewModelCommand(ExecuteCancelDeleteUser, CanExecuteCancelDeleteUser);
        EditCommand = new ViewModelCommand(ExecuteEditCommand);
        AcceptEditCommand = new ViewModelCommand(ExecuteAcceptEditCommand);
        CancelEditCommand = new ViewModelCommand(ExecuteCancelEditCommand);
    }

    private void ExecuteCancelEditCommand(object obj)
    {
        UserName = _oldUser.UserName;
        Email = _oldUser.UserEmail;
        Role = _oldUser.UserRole;
        AchievementsScore = _oldUser.UserAchievementsScore;

        IsEditButtonVisible = true;
        AreAcceptCancelEditButtonsVisible = false;
    }

    private async void ExecuteAcceptEditCommand(object obj)
    {
        await UpdateUserData();

        IsEditButtonVisible = true;
        AreAcceptCancelEditButtonsVisible = false;
    }

    private async Task UpdateUserData()
    {
        var httpClient = App.AppHost!.Services.GetRequiredService<HttpClientService>();
        var route = $"/user/{UserId}";

        var currentUser = new UserWithAllData
        {
            UserId = UserId,
            UserName = UserName,
            UserEmail = Email,
            UserRole = Role,
            UserAchievementsScore = AchievementsScore
        };

        var response = await httpClient.PutAsync(currentUser, route);

#if DEBUG
        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            // Updated successfully
        }
        else
        {
            MessageBox.Show("Error occurred while updating user");
        }
#endif
    }

    private void ExecuteEditCommand(object obj)
    {
        _oldUser = new UserWithAllData
        {
            UserId = UserId,
            UserName = UserName,
            UserEmail = Email,
            UserRole = Role,
            UserAchievementsScore = AchievementsScore
        };

        IsEditButtonVisible = false;
        AreAcceptCancelEditButtonsVisible = true;
    }
}