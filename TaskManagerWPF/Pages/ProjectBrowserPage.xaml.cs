using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManagerWPF.Assets;
using TaskManagerWPF.Assets.CustomControl;

namespace TaskManagerWPF.Pages
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

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = e.OriginalSource as NavButton;

            NavigationService?.Navigate(clickedButton?.NavButtonUri);
        }
    }
}
