using System;
using System.Windows.Input;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.SimpleViewModels;

public class ProjectBindableViewModel : ViewModelBase
{
    private int _projectId;
    private string _projectName = null!;
    private string _projectDescription = null!;
    private DateTime _projectStartDate;
    private DateTime _projectFinishDate;
    private bool _projectCompletionStatus;
    private bool _isDeleteButtonVisible = true;
    private bool _isAcceptCancelButtonsVisible;

    // Properties
    public int ProjectId
    {
        get => _projectId;
        set => SetField(ref _projectId, value);
    }
    public string ProjectName
    {
        get => _projectName;
        set => SetField(ref _projectName, value);
    }
    public string ProjectDescription
    {
        get => _projectDescription;
        set => SetField(ref _projectDescription, value);
    }
    public DateTime ProjectStartDate
    {
        get => _projectStartDate;
        set => SetField(ref _projectStartDate, value);
    }
    public DateTime ProjectFinishDate
    {
        get => _projectFinishDate;
        set => SetField(ref _projectFinishDate, value);
    }
    public bool ProjectCompletionStatus
    {
        get => _projectCompletionStatus;
        set => SetField(ref _projectCompletionStatus, value);
    }
    public bool IsDeleteButtonVisible
    {
        get => _isDeleteButtonVisible;
        set => SetField(ref _isDeleteButtonVisible, value);
    }
    public bool IsAcceptCancelButtonsVisible
    {
        get => _isAcceptCancelButtonsVisible;
        set => SetField(ref _isAcceptCancelButtonsVisible, value);
    }

    // Commands
    public ICommand DeleteProjectCommand { get; set; }
    public ICommand AcceptDeleteCommand { get; set; }
    public ICommand CancelDeleteCommand { get; set; }

    // Constructors
    public ProjectBindableViewModel(Project project)
    {
        DeleteProjectCommand = new ViewModelCommand(ExecuteDeleteProjectCommand, CanExecuteDeleteProjectCommand);
        AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
        CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);

        ProjectId = project.ProjectId;
        ProjectName = new string(project.ProjectName);
        ProjectDescription = new string(project.ProjectDescription);
        ProjectStartDate = project.ProjectStartDate;
        ProjectFinishDate = project.ProjectFinishDate;
        ProjectCompletionStatus = project.ProjectCompletionStatus;
    }

    private void ExecuteDeleteProjectCommand(object obj)
    {
        IsDeleteButtonVisible = false;
        IsAcceptCancelButtonsVisible = true;
    }
    
    private bool CanExecuteDeleteProjectCommand(object obj)
    {
        return _isDeleteButtonVisible;
    }

    private bool CanExecuteCancelDeleteCommand(object obj)
    {
        return !_isDeleteButtonVisible;
    }
    
    private void ExecuteCancelDeleteCommand(object obj)
    {
        IsDeleteButtonVisible = true;
        IsAcceptCancelButtonsVisible = false;
    }

    private bool CanExecuteAcceptDeleteCommand(object obj)
    {
        return !_isDeleteButtonVisible;
    }

    private void ExecuteAcceptDeleteCommand(object obj)
    {
        IsDeleteButtonVisible = true;
        IsAcceptCancelButtonsVisible = false;
    }
}