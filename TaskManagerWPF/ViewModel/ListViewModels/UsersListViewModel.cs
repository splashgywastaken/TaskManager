using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class UsersListViewModel : ViewModelBase
{
    private ObservableCollection<AdminPanelUserListViewModel> _users = null!;

    public ObservableCollection<AdminPanelUserListViewModel> Users
    {
        get => _users;
        set => SetField(ref _users, value);
    }

    public UsersListViewModel(IEnumerable<UserWithAllData> users)
    {
        _users = new ObservableCollection<AdminPanelUserListViewModel>();
        foreach (var user in users)
        {
            _users.Add(new AdminPanelUserListViewModel(user));
        }
    }
}