using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.Model.User;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;
using TaskManagerWPF.ViewModel.ListViewModels;
using Task = System.Threading.Tasks.Task;

namespace TaskManagerWPF.ViewModel
{
    public class ProjectsViewModel : ViewModelBase
    {
        // Private fields
        private readonly UserDataModel _user;

        // Public fields
        public string Title { get; } = "Projects";

        // Property related private fields
        private ProjectListViewModel _projectsListViewModel = null!;
        private bool _isDeleteButtonVisible = true;
        private bool _isAcceptCancelButtonsVisible;

        // Properties
        public ProjectListViewModel ProjectsListViewModel
        {
            get => _projectsListViewModel;
            set
            {
                _projectsListViewModel = value;
                OnPropertyChanged();
            }
        }

        public bool IsDeleteButtonVisible
        {
            get => IsDeleteButtonVisible;
            set
            {
                _isDeleteButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsAcceptCancelButtonsVisible
        {
            get => _isAcceptCancelButtonsVisible;
            set
            {
                _isAcceptCancelButtonsVisible = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand OpenProjectCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand AcceptDeleteCommand { get; set; }
        public ICommand CancelDeleteCommand { get; set; }

        public ProjectsViewModel()
        {
            OpenProjectCommand = new ViewModelCommand(ExecuteOpenProjectCommand, CanExecuteOpenProjectCommand);
            DeleteProjectCommand = new ViewModelCommand(ExecuteDeleteProjectCommand, CanExecuteDeleteProjectCommand);
            AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
            CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);

            _user = App.AppHost!.Services.GetService<UserDataAccess>()!.UserDataModel;
            Init();
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

        private async void ExecuteOpenProjectCommand(object obj)
        {
            var projectId = (int)obj;

            var httpClient = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = $"/project/{projectId}/all";

            var response = await httpClient.GetAsync(route);

            var deserializedResponse = await HttpClientService.DeserializeResponse<ProjectWithAllData>(response);

            Console.WriteLine("open executed");
        }

        private void ExecuteDeleteProjectCommand(object obj)
        {
            IsDeleteButtonVisible = false;
            IsAcceptCancelButtonsVisible = true;
        }

        private bool CanExecuteOpenProjectCommand(object obj)
        {
            return true;
        }
        private bool CanExecuteDeleteProjectCommand(object obj)
        {
            return _isDeleteButtonVisible;
        }

        private void Init()
        {
            LoadProjects();
        }

        private async void LoadProjects()
        {
            var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = $"/user/{_user.UserId}/projects";

            var response = await httpClientService.GetAsync(route);
            
            var deserializeResponse = await HttpClientService.DeserializeResponse<UserProjectsModel>(response);

            var projects = deserializeResponse.UserProjects;

            ProjectsListViewModel = new ProjectListViewModel(projects);
        }
    }
}
