using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerWPF.ViewModel.Base;
using TaskManagerWPF.ViewModel.ListViewModels;

namespace TaskManagerWPF.ViewModel
{
    public class ProjectPageViewModel : ViewModelBase
    {
        private string _projectName = "Sample name";
        private string _projectDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ";
        private ProjectTaskGroupsViewModel _projectTaskGroupsViewModel = null!;

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
    }
}
