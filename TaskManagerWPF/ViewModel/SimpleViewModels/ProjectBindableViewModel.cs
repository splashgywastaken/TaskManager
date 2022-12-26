using System;
using System.Windows.Input;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.ViewModel.Base;

namespace TaskManagerWPF.ViewModel.SimpleViewModels;

public class ProjectBindableViewModel : ViewModelBase
{
    private readonly ProjectsViewModel _parentViewModel;

    private int _projectId;
    private string _projectName = null!;
    private string _projectDescription = null!;
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
    public ProjectBindableViewModel(ProjectsViewModel parentViewModel, Project project)
    {
        _parentViewModel = parentViewModel;

        DeleteProjectCommand = new ViewModelCommand(ExecuteDeleteProjectCommand, CanExecuteDeleteProjectCommand);
        AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
        CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);

        ProjectId = project.ProjectId;
        ProjectName = new string(project.ProjectName);
        ProjectDescription = new string(project.ProjectDescription);
    }

    private async void ExecuteDeleteProjectCommand(object obj)
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

    private async void ExecuteAcceptDeleteCommand(object obj)
    {
        await _parentViewModel.DeleteProjectById((int) obj);

        IsDeleteButtonVisible = true;
        IsAcceptCancelButtonsVisible = false;
    }
}