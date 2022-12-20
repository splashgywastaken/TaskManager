using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.Services.DataAccess;
using TaskManagerWPF.View;
using TaskManagerWPF.View.Windows;
using TaskManagerWPF.ViewModel;

namespace TaskManagerWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewDataService = App.AppHost!.Services.GetRequiredService<ViewDataService>();
            viewDataService.AddView(DataContext, "MainViewModel");
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void ButtonMinimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
