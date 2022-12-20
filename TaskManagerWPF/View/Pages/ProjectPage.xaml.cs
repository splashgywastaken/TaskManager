using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TaskManagerWPF.Assets.CustomControl;
using TaskManagerWPF.Model.Project;

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
            InitializeComponent();

            ProjectId = projectId;
        }
        
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton?.NavButtonUri);
        }
    }
}
