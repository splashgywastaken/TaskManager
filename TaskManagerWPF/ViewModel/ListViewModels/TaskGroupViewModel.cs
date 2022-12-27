using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;
using Task = System.Threading.Tasks.Task;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class TaskGroupViewModel : ViewModelBase
{
    // Everything related to how UI works
    #region UI properties
    // Private properties fields
    private bool _isDeleteButtonVisible = true;
    private bool _areAcceptCancelDeleteButtonsVisible;
    private bool _isEditButtonVisible = true;
    private bool _areAcceptCancelEditButtonsVisible;

    // Properties
    public bool IsDeleteButtonVisible
    {
        get => _isDeleteButtonVisible;
        set => SetField(ref _isDeleteButtonVisible, value);
    }
    public bool AreAcceptCancelDeleteButtonsVisible
    {
        get => _areAcceptCancelDeleteButtonsVisible;
        set => SetField(ref _areAcceptCancelDeleteButtonsVisible, value);
    }
    public bool IsEditButtonVisible
    {
        get => _isEditButtonVisible;
        set => SetField(ref _isEditButtonVisible, value);
    }
    public bool AreAcceptCancelEditButtonsVisible
    {
        get => _areAcceptCancelEditButtonsVisible;
        set => SetField(ref _areAcceptCancelEditButtonsVisible, value);
    }
    #endregion
    #region DataFields
    private TaskGroupWithAllData _oldTaskGroup = null!;
    #endregion
    #region DataProperties
    // Private properties fields
    private int _taskGroupId;
    private string _taskGroupName = null!;
    private string _taskGroupDescription = null!;
    private ObservableCollection<TaskListViewModel> _taskList = null!;
    private ProjectTaskGroupsViewModel _parentViewModel;
    // Properties
    public int TaskGroupId
    {
        get => _taskGroupId;
        set => SetField(ref _taskGroupId, value);
    }
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
    #endregion
    #region Commands
    public ICommand DeleteCommand { get; set; }
    public ICommand CancelDeleteCommand { get; set; }
    public ICommand AcceptDeleteCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand CancelEditCommand { get; set; }
    public ICommand AcceptEditCommand { get; set; }
    public ICommand AddNewTaskCommand { get; set; }
    #endregion
    #region CommandsMethods
    private bool CanExecuteAcceptEditCommand(object obj)
    {
        return AreAcceptCancelEditButtonsVisible;
    }
    private bool CanExecuteCancelEditCommand(object obj)
    {
        return AreAcceptCancelEditButtonsVisible;
    }
    private bool CanExecuteEditCommand(object obj)
    {
        return IsEditButtonVisible;
    }
    private bool CanExecuteAcceptDeleteCommand(object obj)
    {
        return AreAcceptCancelDeleteButtonsVisible && _parentViewModel.TaskGroups.Count != 1;
    }
    private bool CanExecuteCancelDeleteCommand(object obj)
    {
        return AreAcceptCancelDeleteButtonsVisible;
    }
    private bool CanExecuteDeleteCommand(object obj)
    {
        return IsDeleteButtonVisible;
    }
    private async void ExecuteAcceptEditCommand(object obj)
    {
        // TODO: Test new code
        var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
        var taskGroupProjectId = _parentViewModel.ProjectId;
        var route = $"/project/{taskGroupProjectId}/taskGroup/{TaskGroupId}";

        var newTaskGroup = new TaskGroupWithAllData
        {
            TaskGroupProjectId = taskGroupProjectId,
            TaskGroupId = TaskGroupId,
            TaskGroupName = TaskGroupName,
            TaskGroupDescription = TaskGroupDescription
        };

        var response = await httpClientService.PutAsync(newTaskGroup, route);

        if (!response.IsSuccessStatusCode)
        {
            MessageBox.Show($"Couldn't update taskGroup #{TaskGroupId} " +
                            $"from project #{taskGroupProjectId} data");
            return;
        }

        AreAcceptCancelEditButtonsVisible = false;
        IsEditButtonVisible = true;
    }
    private void ExecuteCancelEditCommand(object obj)
    {
        TaskGroupName = _oldTaskGroup.TaskGroupName;
        TaskGroupDescription = _oldTaskGroup.TaskGroupDescription;

        AreAcceptCancelEditButtonsVisible = false;
        IsEditButtonVisible = true;
    }
    private void ExecuteEditCommand(object obj)
    {
        _oldTaskGroup = new TaskGroupWithAllData(_taskGroupId, TaskGroupName, TaskGroupDescription);


        AreAcceptCancelEditButtonsVisible = true;
        IsEditButtonVisible = false;
    }
    private async void ExecuteAcceptDeleteCommand(object obj)
    {
        // TODO: test new code
        await _parentViewModel.DeleteTaskGroupById(TaskGroupId);

        AreAcceptCancelDeleteButtonsVisible = false;
        IsDeleteButtonVisible = true;
    }
    private void ExecuteCancelDeleteCommand(object obj)
    {
        AreAcceptCancelDeleteButtonsVisible = false;
        IsDeleteButtonVisible = true;
    }
    private void ExecuteDeleteCommand(object obj)
    {
        AreAcceptCancelDeleteButtonsVisible = true;
        IsDeleteButtonVisible = false;
    }
    private async void ExecuteAddNewTaskCommand(object obj)
    {
        await AddNewTask();
    }
    #endregion
    public TaskGroupViewModel(ProjectTaskGroupsViewModel parentViewModel, TaskGroupWithAllData taskGroup)
    {
        DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);
        AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
        EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
        CancelEditCommand = new ViewModelCommand(ExecuteCancelEditCommand, CanExecuteCancelEditCommand);
        AcceptEditCommand = new ViewModelCommand(ExecuteAcceptEditCommand, CanExecuteAcceptEditCommand);
        AddNewTaskCommand = new ViewModelCommand(ExecuteAddNewTaskCommand);

        _taskGroupId = taskGroup.TaskGroupId;
        TaskGroupName = new string(taskGroup.TaskGroupName);
        TaskGroupDescription = new string(taskGroup.TaskGroupDescription);
        TaskList = new ObservableCollection<TaskListViewModel>();
        foreach (var task in taskGroup.TaskGroupTasks)
        {
            TaskList.Add(new TaskListViewModel(this, task));
        }

        _parentViewModel = parentViewModel;
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

    private async Task AddNewTask()
    {
        var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
        var route = $"/taskGroup/{TaskGroupId}/task";

        var newTask = new TaskWithAllData
        {
            TaskTaskGroupId = TaskGroupId,
            TaskName = "Task name",
            TaskDescription = "Task description",
            TaskStartDate = DateTime.Now,
            TaskFinishDate = DateTime.Now.AddDays(30),
            TaskCompletionStatus = false
        };

        var response = await httpClientService.PostAsync(newTask, route);

        var newTaskDeserialized = await HttpClientService.DeserializeResponse<TaskWithAllData>(response);
        newTask.TaskId = newTaskDeserialized.TaskId;

        if (!response.IsSuccessStatusCode)
        {
            MessageBox.Show($"Cannot add task to taskGroup {TaskGroupId}");
            return;
        }

        TaskList.Add(new TaskListViewModel(this, newTask));
    }

    public async Task DeleteTaskById(int taskId)
    {
        var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
        var route = $"/task/{taskId}";

        var response = await httpClientService.DeleteAsync(route);

        if (!response.IsSuccessStatusCode)
        {
            MessageBox.Show($"Cannot delete task {taskId}");
            return;
        }

        foreach (var task in TaskList)
        {
            if (task.TaskId != taskId) continue;
            TaskList.Remove(task);
            break;
        }
    }
}