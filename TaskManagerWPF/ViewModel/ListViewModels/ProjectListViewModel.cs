using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Transactions;
using TaskManagerWPF.Model;
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

    public ProjectListViewModel(IEnumerable<Project> projects)
    {
        AreProjectsLoaded = false;
        _projects = new ObservableCollection<ProjectBindableViewModel>();
        foreach (var project in projects)
        {
            _projects.Add(new ProjectBindableViewModel(project));
        }
        AreProjectsLoaded = true;
    }
}