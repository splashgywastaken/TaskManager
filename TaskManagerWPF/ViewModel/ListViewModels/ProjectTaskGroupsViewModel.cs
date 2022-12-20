using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Options;
using TaskManagerWPF.Model;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class ProjectTaskGroupsViewModel : ViewModelBase
{
    private ObservableCollection<TaskGroupViewModel> _taskGroups = null!;

    public ObservableCollection<TaskGroupViewModel> TaskGroups
    {
        get => _taskGroups;
        set => SetField(ref _taskGroups, value);
    }

    public ProjectTaskGroupsViewModel(IEnumerable<TaskGroupWithAllData> taskGroups)
    {
        TaskGroups = new ObservableCollection<TaskGroupViewModel>();
        foreach (var taskGroup in taskGroups)
        {
            TaskGroups.Add(new TaskGroupViewModel(taskGroup));
        }
    }
}