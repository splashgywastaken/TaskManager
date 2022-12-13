using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerWPF.ViewModel;

namespace TaskManagerWPF.View.Windows
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void ButtonMinimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void AuthWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnClosing(CancelEventArgs e) { }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonSignUp_OnClick(object sender, RoutedEventArgs e)
        {
            var signUpWindow = App.AppHost!.Services.GetRequiredService<SignUpWindow>();
            ((signUpWindow.DataContext as SignUpWindowViewModel)!).ParentViewModel = DataContext as AuthWindowViewModel;
            signUpWindow.Show();
        }
    }
}
