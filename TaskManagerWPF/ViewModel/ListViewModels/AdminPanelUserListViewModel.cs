using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class AdminPanelUserListViewModel : ViewModelBase
{
    private UsersListViewModel _parentViewModel;

    #region DataProperties
    private int _userId;
    private string _userName = null!;
    private string _email = null!;
    private int _achievementsScore;
    private string _role = null!;
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
    #endregion
    #region UIProperties
    private bool _isDeleteButtonVisible = true;
    private bool _areAcceptCancelButtonsVisible;
    public bool IsDeleteButtonVisible
    {
        get => _isDeleteButtonVisible;
        set
        {
            _isDeleteButtonVisible = value;
            OnPropertyChanged();
        }
    }
    public bool AreAcceptCancelButtonsVisible
    {
        get => _areAcceptCancelButtonsVisible;
        set
        {
            _areAcceptCancelButtonsVisible = value;
            OnPropertyChanged();
        }
    }
    #endregion
    #region Commands
    public ICommand DeleteUser { get; set; }
    public ICommand AcceptDeleteUser { get; set; }
    public ICommand CancelDeleteUser { get; set; }
    #endregion
    #region CommandsMethods
    private bool CanExecuteCancelDeleteUser(object obj)
    {
        return AreAcceptCancelButtonsVisible;
    }
    private void ExecuteCancelDeleteUser(object obj)
    {
        AreAcceptCancelButtonsVisible = false;
        IsDeleteButtonVisible = true;
    }
    private bool CanExecuteAcceptDeleteUser(object obj)
    {
        return AreAcceptCancelButtonsVisible;
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
        AreAcceptCancelButtonsVisible = false;
        IsDeleteButtonVisible = true;
    }
    private bool CanExecuteDeleteUser(object obj)
    {
        return IsDeleteButtonVisible;
    }
    private void ExecuteDeleteUser(object obj)
    {
        AreAcceptCancelButtonsVisible = true;
        IsDeleteButtonVisible = false;
    }
    #endregion

    public AdminPanelUserListViewModel(UsersListViewModel parentViewModel, UserWithAllData user)
    {
        _parentViewModel = parentViewModel;

        _userId = user.UserId;
        _userName = new string(user.UserName);
        _email = new string(user.UserEmail);
        _achievementsScore = AchievementsScore;
        _role = new string(user.UserRole);

        DeleteUser = new ViewModelCommand(ExecuteDeleteUser, CanExecuteDeleteUser);
        AcceptDeleteUser = new ViewModelCommand(ExecuteAcceptDeleteUser, CanExecuteAcceptDeleteUser);
        CancelDeleteUser = new ViewModelCommand(ExecuteCancelDeleteUser, CanExecuteCancelDeleteUser);
    }

}