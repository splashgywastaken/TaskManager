using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.Services.Web;
using TaskManagerWPF.ViewModel.Base;
using TaskManagerWPF.ViewModel.ListViewModels;

namespace TaskManagerWPF.ViewModel
{
    public class ProjectPageViewModel : ViewModelBase
    {
        private int _projectId;
        private string _projectName = "Sample name";
        private string _projectDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ";
        private ProjectTaskGroupsViewModel _projectTaskGroupsViewModel = null!;
        private bool _isDeleteButtonVisible = true;
        private bool _areAcceptCancelDeleteButtonsVisible;
        private bool _isEditButtonVisible = true;
        private bool _areAcceptCancelEditButtonsVisible;

        public int ProjectId
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

        public ICommand DeleteCommand { get; set; }
        public ICommand CancelDeleteCommand { get; set; }
        public ICommand AcceptDeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelEditCommand { get; set; }
        public ICommand AcceptEditCommand { get; set; }

        public ProjectPageViewModel()
        {
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            CancelDeleteCommand = new ViewModelCommand(ExecuteCancelDeleteCommand, CanExecuteCancelDeleteCommand);
            AcceptDeleteCommand = new ViewModelCommand(ExecuteAcceptDeleteCommand, CanExecuteAcceptDeleteCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
            CancelEditCommand = new ViewModelCommand(ExecuteCancelEditCommand, CanExecuteCancelEditCommand);
            AcceptEditCommand = new ViewModelCommand(ExecuteAcceptEditCommand, CanExecuteAcceptEditCommand);
        }

        public async Task LoadProjects(int projectId)
        {
            ProjectId = projectId;

            var httpClientService = App.AppHost!.Services.GetRequiredService<HttpClientService>();
            var route = $"/project/{projectId}/all";

            var response = await httpClientService.GetAsync(route);

            var projectWithAllData = await HttpClientService.DeserializeResponse<ProjectWithAllData>(response);

            ProjectName = new (projectWithAllData.ProjectName);
            ProjectDescription = new (projectWithAllData.ProjectDescription);
            ProjectTaskGroupsViewModel = new ProjectTaskGroupsViewModel(projectWithAllData.ProjectTaskGroups);
        }

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
            AreAcceptCancelEditButtonsVisible = false;
            IsEditButtonVisible = true;
        }

        private void ExecuteCancelEditCommand(object obj)
        {
            AreAcceptCancelEditButtonsVisible = false;
            IsEditButtonVisible = true;
        }

        private void ExecuteEditCommand(object obj)
        {
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
    }
}
