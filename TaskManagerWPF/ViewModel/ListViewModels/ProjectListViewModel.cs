using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.ViewModel.Base;
using TaskManagerWPF.ViewModel.SimpleViewModels;

namespace TaskManagerWPF.ViewModel.ListViewModels;

public class ProjectListViewModel : ViewModelBase
{
    private ObservableCollection<ProjectBindableViewModel> _projects;
    private bool _areProjectsLoaded;

    public ObservableCollection<ProjectBindableViewModel> Projects
    {
        get => _projects;
        set => SetField(ref _projects, value);
    }

    public bool AreProjectsLoaded
    {
        get => _areProjectsLoaded;
        set => SetField(ref _areProjectsLoaded, value);
    }

    public void AddProject(ProjectsViewModel parentViewModel, Project project)
    {
        Projects.Add(new ProjectBindableViewModel(parentViewModel, project));
    }

    public ProjectListViewModel(ProjectsViewModel parentViewModel, IEnumerable<Project> projects)
    {
        AreProjectsLoaded = false;
        _projects = new ObservableCollection<ProjectBindableViewModel>();
        foreach (var project in projects)
        {
            AddProject(parentViewModel, project);
        }
        AreProjectsLoaded = true;
    }
}