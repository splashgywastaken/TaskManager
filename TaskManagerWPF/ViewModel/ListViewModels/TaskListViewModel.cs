using System.Collections.ObjectModel;
using TaskManagerWPF.Model;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class TaskListViewModel : ViewModelBase
{
    private string _taskName = null!;
    private string _taskDescription = null!;

    public string TaskName
    {
        get => _taskName;
        set => SetField(ref _taskName, value);
    }

    public string TaskDescription
    {
        get => _taskDescription;
        set => SetField(ref _taskDescription, value);
    }
}