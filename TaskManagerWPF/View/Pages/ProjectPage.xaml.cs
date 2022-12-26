using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TaskManagerWPF.Assets.CustomControl;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.ViewModel;
using TaskManagerWPF.ViewModel.Pages;

namespace TaskManagerWPF.View.Pages
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        private int ProjectId { get; set; }

        public ProjectPage(ProjectsViewModel parentViewModel, int projectId)
        {
            ProjectId = projectId;
            InitializeComponent();
            var dataContext = (DataContext as ProjectPageViewModel)!;
            dataContext.ParentViewModel = parentViewModel;
        }
        
        protected override async void OnInitialized(EventArgs e)
        {
            var dataContext = (DataContext as ProjectPageViewModel)!;
            await dataContext.LoadProjects(ProjectId);
            base.OnInitialized(e);
        }

        private void BackToProjectBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not NavButton {Name: "BackToProjectBrowserButton" } clickedButton) return;

            NavigationService?.Navigate(clickedButton?.NavButtonUri);
        }

        private void CompletionStatusButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            if (DataContext is not TaskViewModel task) return;

            if (task.TaskCompletionStatus)
            {
                task.TaskCompletionStatus = false;
                button.Style = FindResource("CompletionStatusButtonUncheckedStyle") as Style;
            }
            else
            {
                task.TaskCompletionStatus = true;
                button.Style = FindResource("CompletionStatusButtonCheckedStyle") as Style;
            }
        }

        private void AcceptDeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var uri = BackToProjectBrowserButton.NavButtonUri;
            NavigationService?.Navigate(uri);
        }
    }
}
