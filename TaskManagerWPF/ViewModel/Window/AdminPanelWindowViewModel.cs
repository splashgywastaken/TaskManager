using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;
using TaskManagerWPF.ViewModel.ListViewModels;

namespace TaskManagerWPF.ViewModel.Window;

public class AdminPanelWindowViewModel : ViewModelBase
{
    private bool _isViewVisible = true;
    private UsersListViewModel _userListViewModel = null!;
    
    public bool IsViewVisible
    {
        get => _isViewVisible;
        set => SetField(ref _isViewVisible, value);
    }
    
    public UsersListViewModel UserListViewModel
    {
        get => _userListViewModel;
        set => SetField(ref _userListViewModel, value);
    }

    public AdminPanelWindowViewModel()
    {
        LoadUsers();
    }

    public async void LoadUsers()
    {
        var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
        const string url = "/user/all";

        var response = await httpClientService.GetAsync(url);
        var users = await HttpClientService.DeserializeResponse<List<UserWithAllData>>(response);

        UserListViewModel = new UsersListViewModel(users);
    }
}