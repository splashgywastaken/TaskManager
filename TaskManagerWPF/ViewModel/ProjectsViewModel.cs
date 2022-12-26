using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SharpVectors.Converters;
using TaskManagerWPF.Model.PostModels;
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

        #region Properties related stuff
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
        #endregion

        // Commands
        #region Commands
        public ICommand OpenProjectCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand AcceptDeleteCommand { get; set; }
        public ICommand CancelDeleteCommand { get; set; } 
        public ICommand AddNewProjectCommand { get; set; }
        #endregion

        public ProjectsViewModel()
        {
            OpenProjectCommand = new ViewModelCommand(ExecuteOpenProjectCommand, CanExecuteOpenProjectCommand);
            DeleteProjectCommand = new ViewModelCommand(ExecuteDeleteProjectCommand, CanExecuteDeleteProjectCommand);
            AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
            CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);
            AddNewProjectCommand = new ViewModelCommand(ExecuteAddNewProject);

            _user = UserDataAccess.UserDataModel;
            Init();
        }

        #region Command methods
        private async void ExecuteAddNewProject(object obj)
        {
            await AddNewProject();
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
            await DeleteProjectById((int)obj);

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
        #endregion

        private void Init()
        {
            LoadProjects();
        }

        private async Task AddNewProject()
        {
            // http backend stuff
            var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = "/user/projects";
            var data = new CompositeProjectTaskGroupModel
            {
                ProjectPostModel = new ProjectPostModel
                {
                    ProjectUserId = UserDataAccess.UserDataModel.UserId,
                    ProjectName = "Project name",
                    ProjectDescription = "Project description"
                },
                TaskGroupPostModel = new TaskGroupPostModel
                {
                    TaskGroupName = "Task group name",
                    TaskDescription = "Task group description"
                }
            };

            var response = await httpClientService.PostAsync(data, route);

            var project = await HttpClientService.DeserializeResponse<Project>(response);

            route = $"/project/{project.ProjectId}/all";
            response = await httpClientService.GetAsync(route);

            var projectData = await HttpClientService.DeserializeResponse<ProjectWithAllData>(response);
            var taskGroupId = projectData.ProjectTaskGroups.Last().TaskGroupId;

            // Adding new task to added task group
            var newTask = new TaskWithAllData
            {
                TaskTaskGroupId = taskGroupId,
                TaskName = "Task name",
                TaskDescription = "Task description",
                TaskStartDate = DateTime.Now,
                TaskFinishDate = DateTime.Now.AddDays(30),
                TaskCompletionStatus = false
            };
            route = $"/taskGroup/{taskGroupId}/task";
            response = await httpClientService.PostAsync(newTask, route);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Cannot add base task to a new task group");
                return;
            }

            var postedTask = await HttpClientService.DeserializeResponse<TaskWithAllData>(response);

            ProjectsListViewModel.AddProject(this, project);
        }

        private async void LoadProjects()
        {
            var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = $"/user/{_user.UserId}/projects";

            var response = await httpClientService.GetAsync(route);
            
            var deserializeResponse = await HttpClientService.DeserializeResponse<UserProjectsModel>(response);

            var projects = deserializeResponse.UserProjects;

            ProjectsListViewModel = new ProjectListViewModel(this, projects);
        }

        public async Task DeleteProjectById(int projectId)
        {
            var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = $"/project/{projectId}";

            var response = await httpClientService.DeleteAsync(route);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Failed deleting project");
                return;
            }

            foreach (var project in ProjectsListViewModel.Projects)
            {
                if (project.ProjectId != projectId) continue;
                ProjectsListViewModel.Projects.Remove(project);
                break;
            }
        }
    }
}
