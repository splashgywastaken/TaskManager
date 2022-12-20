using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Documents;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class TaskGroupViewModel : ViewModelBase
{
    private string _taskGroupName = null!;
    private string _taskGroupDescription = null!;
    private ObservableCollection<TaskListViewModel> _taskList = null!;

    public string TaskGroupName
    {
        get => _taskGroupName;
        set => SetField(ref _taskGroupName, value);
    }

    public string TaskGroupDescription
    {
        get => _taskGroupDescription;
        set => SetField(ref _taskGroupDescription, value);
    }

    public ObservableCollection<TaskListViewModel> TaskList
    {
        get => _taskList;
        set => SetField(ref _taskList, value);
    }

    public ObservableCollection<TaskListViewModel> TaskTags
    {
        get => _taskList;
        set
        {
            _taskList = value;
            SetField(ref _taskList, value);
        }
    }
}