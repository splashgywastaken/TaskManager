using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.IconPacks.Converter;
using TaskManagerWPF.Model;
using TaskManagerWPF.ViewModel;

namespace TaskManagerWPF.View.ModelViews
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl
    {
        public TaskView()
        {
            InitializeComponent();

            CompletionStatusButton.Style = FindResource("CompletionStatusButtonUncheckedStyle") as Style;
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
    }
}
