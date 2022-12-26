using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class ProjectTaskGroupsViewModel : ViewModelBase
{
    private ObservableCollection<TaskGroupViewModel> _taskGroups = null!;
    public int ProjectId { get; set; }

    public ObservableCollection<TaskGroupViewModel> TaskGroups
    {
        get => _taskGroups;
        set => SetField(ref _taskGroups, value);
    }

    public ProjectTaskGroupsViewModel(int projectId, IEnumerable<TaskGroupWithAllData> taskGroups)
    {
        TaskGroups = new ObservableCollection<TaskGroupViewModel>();
        ProjectId = projectId;
        foreach (var taskGroup in taskGroups)
        {
            AddNewTaskGroup(taskGroup);
        }
    }

    public void AddNewTaskGroup(TaskGroupWithAllData taskGroup)
    {
        TaskGroups.Add(new TaskGroupViewModel(this, taskGroup));
    }

    public async Task DeleteTaskGroupById(int taskGroupId)
    {
        var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
        var route = $"/taskGroup/{taskGroupId}";

        var response = await httpClientService.DeleteAsync(route);

        if (!response.IsSuccessStatusCode)
        {
            MessageBox.Show("Cannot delete taskGroup");
            return;
        }

        foreach (var taskGroup in TaskGroups)
        {
            if (taskGroup.TaskGroupId != taskGroupId) continue;
            TaskGroups.Remove(taskGroup);
            break;
        }
    }
}