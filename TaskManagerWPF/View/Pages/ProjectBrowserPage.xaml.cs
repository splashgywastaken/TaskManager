using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using TaskManagerWPF.Assets.CustomControl;
using TaskManagerWPF.Services.Misc;
using TaskManagerWPF.ViewModel;

namespace TaskManagerWPF.View.Pages
{
    /// <summary>
    /// Interaction logic for ProjectBrowserPage.xaml
    /// </summary>
    public partial class ProjectBrowserPage : Page
    {
        public ProjectBrowserPage()
        {
            InitializeComponent();
        }

        private void OpenProjectButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not NavButton {Name: "OpenProjectButton"} clickedButton) return;

            var projectId = (int)clickedButton.CommandParameter;
            NavigationService!.Navigate(
                new ProjectPage(
                        DataContext as ProjectsViewModel ?? throw new InvalidOperationException(),
                        projectId
                    ));
        }
    }
}
