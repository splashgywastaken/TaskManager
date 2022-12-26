using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;
using TaskManagerWPF.ViewModel.ListViewModels;

namespace TaskManagerWPF.ViewModel.Pages
{
    public class ProjectPageViewModel : ViewModelBase
    {
        #region Private fields
        private ProjectWithAllData _oldProject = null!;
        private int _projectId;
        private string _projectName = "Sample name";
        private string _projectDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ";
        private ProjectsViewModel _parentViewModel = null!;
        private ProjectTaskGroupsViewModel _projectTaskGroupsViewModel = null!;
        private bool _isDeleteButtonVisible = true;
        private bool _areAcceptCancelDeleteButtonsVisible;
        private bool _isEditButtonVisible = true;
        private bool _areAcceptCancelEditButtonsVisible;
        #endregion

        #region Properties
        private int ProjectId
        {
            get => _projectId;
            set => SetField(ref _projectId, value);
        }
        public string ProjectName
        {
            get => _projectName;
            set
            {
                _projectName = value;
                OnPropertyChanged();
            }
        }
        public string ProjectDescription
        {
            get => _projectDescription;
            set
            {
                _projectDescription = value;
                OnPropertyChanged();
            }
        }
        public ProjectsViewModel ParentViewModel
        {
            get => _parentViewModel;
            set => SetField(ref _parentViewModel, value);
        }
        public ProjectTaskGroupsViewModel ProjectTaskGroupsViewModel
        {
            get => _projectTaskGroupsViewModel;
            set
            {
                _projectTaskGroupsViewModel = value;
                OnPropertyChanged();
            }
        }
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

        #region Commands
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelDeleteCommand { get; set; }
        public ICommand AcceptDeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelEditCommand { get; set; }
        public ICommand AcceptEditCommand { get; set; } 
        public ICommand AddNewTaskGroupCommand { get; set; }
        #endregion

        public ProjectPageViewModel()
        {
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);
            AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            CancelEditCommand = new ViewModelCommand(ExecuteCancelEditCommand, CanExecuteCancelEditCommand);
            AcceptEditCommand = new ViewModelCommand(ExecuteAcceptEditCommand, CanExecuteAcceptEditCommand);
            AddNewTaskGroupCommand = new ViewModelCommand(ExecuteAddNewTaskGroupCommand);
        }
        
        public async Task LoadProjects(int projectId)
        {
            ProjectId = projectId;

            var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = $"/project/{projectId}/all";

            var response = await httpClientService.GetAsync(route);

            var projectWithAllData = await HttpClientService.DeserializeResponse<ProjectWithAllData>(response);

            ProjectName = new string(projectWithAllData.ProjectName);
            ProjectDescription = new string(projectWithAllData.ProjectDescription);
            ProjectTaskGroupsViewModel = new ProjectTaskGroupsViewModel(ProjectId, projectWithAllData.ProjectTaskGroups);
        }

        #region Commands CanExecuteAction methods
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
        #endregion

        #region Commands ExecuteAction methods
        private async void ExecuteAddNewTaskGroupCommand(object obj)
        {
            await AddNewTaskGroup();
        }
        private async void ExecuteAcceptEditCommand(object obj)
        {
            var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = $"/project/{ProjectId}";

            var newProjectData = new ProjectWithAllData
            {
                ProjectUserId = UserDataAccess.UserDataModel.UserId,
                ProjectId = ProjectId,
                ProjectName = ProjectName,
                ProjectDescription = ProjectDescription,
                ProjectStartDate = DateTime.UtcNow,
                ProjectFinishDate = DateTime.Now.AddDays(30),
                ProjectCompletionStatus = false
            };

            var response = await httpClientService.PutAsync(newProjectData, route);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Couldn't edit project {ProjectName}");
                ExecuteCancelEditCommand(null);
                return;
            }

            AreAcceptCancelEditButtonsVisible = false;
            IsEditButtonVisible = true;
        }

        private void ExecuteCancelEditCommand(object obj)
        {
            ProjectName = _oldProject.ProjectName;
            ProjectDescription = _oldProject.ProjectDescription;

            AreAcceptCancelEditButtonsVisible = false;
            IsEditButtonVisible = true;
        }

        private void ExecuteEditCommand(object obj)
        {
            _oldProject = new ProjectWithAllData(ProjectId, ProjectName, ProjectDescription);

            AreAcceptCancelEditButtonsVisible = true;
            IsEditButtonVisible = false;
        }

        private async void ExecuteAcceptDeleteCommand(object obj)
        {
            await DeleteProjectById(ProjectId);

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

        private async Task DeleteProjectById(int projectId)
        {
            await ParentViewModel.DeleteProjectById(projectId);
        }

        private async Task AddNewTaskGroup()
        {
            var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();

            // Adding new task to project
            var newTaskGroup = new TaskGroupWithAllData(
                0,
                "Task group name",
                "Task group description"
                );
            var route = $"/project/{ProjectId}/taskGroup";
            var response = await httpClientService.PostAsync(newTaskGroup, route);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Cannot add new task group");
                return;
            }

            var postedTaskGroup = await HttpClientService.DeserializeResponse<TaskGroupWithAllData>(response);

            // Adding new task to added task group
            var newTask = new TaskWithAllData
            {
                TaskTaskGroupId = postedTaskGroup.TaskGroupId,
                TaskName = "Task name",
                TaskDescription = "Task description",
                TaskStartDate = DateTime.Now,
                TaskFinishDate = DateTime.Now.AddDays(30),
                TaskCompletionStatus = false
            };
            route = $"/taskGroup/{postedTaskGroup.TaskGroupId}/task";
            response = await httpClientService.PostAsync(newTask, route);

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Cannot add base task to a new task group");
                return;
            }

            var postedTask = await HttpClientService.DeserializeResponse<TaskWithAllData>(response);

            postedTaskGroup.TaskGroupTasks = new List<TaskWithAllData>
            {
                postedTask
            };

            // Finally adding all data to viewModel data
            ProjectTaskGroupsViewModel.AddNewTaskGroup(postedTaskGroup);
        }
    }
}
