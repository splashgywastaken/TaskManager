using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Documents;
using System.Windows.Input;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.ViewModel.Base;

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
    private readonly int _taskGroupId;
    private string _taskGroupName = null!;
    private string _taskGroupDescription = null!;
    private ObservableCollection<TaskListViewModel> _taskList = null!;
    // Properties
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
        return AreAcceptCancelDeleteButtonsVisible;
    }
    private bool CanExecuteCancelDeleteCommand(object obj)
    {
        return AreAcceptCancelDeleteButtonsVisible;
    }
    private bool CanExecuteDeleteCommand(object obj)
    {
        return IsDeleteButtonVisible;
    }
    private void ExecuteAcceptEditCommand(object obj)
    {
        // Do things with taskgroup data

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
    private void ExecuteAcceptDeleteCommand(object obj)
    {
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
    #endregion
    public TaskGroupViewModel(TaskGroupWithAllData taskGroup)
    {
        DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);
        AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
        EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
        CancelEditCommand = new ViewModelCommand(ExecuteCancelEditCommand, CanExecuteCancelEditCommand);
        AcceptEditCommand = new ViewModelCommand(ExecuteAcceptEditCommand, CanExecuteAcceptEditCommand);

        _taskGroupId = taskGroup.TaskGroupId;
        TaskGroupName = new string(taskGroup.TaskGroupName);
        TaskGroupDescription = new string(taskGroup.TaskGroupDescription);
        TaskList = new ObservableCollection<TaskListViewModel>();
        foreach (var task in taskGroup.TaskGroupTasks)
        {
            TaskList.Add(new TaskListViewModel(task));
        }
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