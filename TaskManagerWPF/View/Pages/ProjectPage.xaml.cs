using System.Windows;
using System.Windows.Controls;
using TaskManagerWPF.Assets.CustomControl;

namespace TaskManagerWPF.View.Pages
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton?.NavButtonUri);
        }
    }
}
