using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskManagerWPF.Model;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class TaskListViewModel : ViewModelBase
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
    #region DataPropertiesAndFields
    // Private fields
    private TaskWithAllData _oldTask = null!;
    // Private fields related to properties
    private int _taskId;
    private string _taskName = null!;
    private string _taskDescription = null!;
    private bool _taskCompletionStatus;
    // Properties
    public int TaskId
    {
        get => _taskId;
        set => SetField(ref _taskId, value);
    }
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
    public bool TaskCompletionStatus
    {
        get => _taskCompletionStatus;
        set => SetField(ref _taskCompletionStatus, value);
    }
    #endregion
    #region Commands
    public ICommand DeleteCommand { get; set; }
    public ICommand CancelDeleteCommand { get; set; }
    public ICommand AcceptDeleteCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand CancelEditCommand { get; set; }
    public ICommand AcceptEditCommand { get; set; }
    public ICommand ChangeCompletionStatusCommand { get; set; }
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
        // Do smth with data

        AreAcceptCancelEditButtonsVisible = false;
        IsEditButtonVisible = true;
    }
    private void ExecuteCancelEditCommand(object obj)
    {
        TaskName = _oldTask.TaskName;
        TaskDescription = _oldTask.TaskDescription;

        AreAcceptCancelEditButtonsVisible = false;
        IsEditButtonVisible = true;
    }
    private void ExecuteEditCommand(object obj)
    {
        _oldTask = new TaskWithAllData(TaskName, TaskDescription);

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
    private void ExecuteChangeCompletionStatusCommand(object obj)
    {
        TaskCompletionStatus = !TaskCompletionStatus;
    }
    #endregion
    public TaskListViewModel(TaskWithAllData task)
    {
        DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);
        AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
        EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
        CancelEditCommand = new ViewModelCommand(ExecuteCancelEditCommand, CanExecuteCancelEditCommand);
        AcceptEditCommand = new ViewModelCommand(ExecuteAcceptEditCommand, CanExecuteAcceptEditCommand);
        ChangeCompletionStatusCommand = new ViewModelCommand(ExecuteChangeCompletionStatusCommand);
        
        TaskId = task.TaskId;
        TaskName = new string(task.TaskName);
        TaskDescription = new string(task.TaskDescription);
        TaskCompletionStatus = task.TaskCompletionStatus;
    }
}