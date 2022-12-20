﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TaskManagerWPF.Assets.CustomControl;
using TaskManagerWPF.Model.Project;
using TaskManagerWPF.ViewModel;

namespace TaskManagerWPF.View.Pages
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public int ProjectId { get; set; }

        public ProjectPage(int projectId)
        {
            ProjectId = projectId;
            InitializeComponent();
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
    }
}
