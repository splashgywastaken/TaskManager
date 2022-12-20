using System.Windows;
using System.Windows.Controls;

namespace TaskManagerWPF.View.ModelViews.SimpleModelViews
{
    /// <summary>
    /// Interaction logic for SimpleProjectView.xaml
    /// </summary>
    public partial class SimpleProjectView : UserControl
    {
        public static readonly DependencyProperty ProjectDescriptionProperty = DependencyProperty.Register(
            nameof(ProjectDescription), typeof(string), typeof(SimpleProjectView), new PropertyMetadata(default(string)));

        public string ProjectDescription
        {
            get => (string) GetValue(ProjectDescriptionProperty);
            set => SetValue(ProjectDescriptionProperty, value);
        }

        public static readonly DependencyProperty ProjectNameProperty = DependencyProperty.Register(
                nameof(ProjectName), 
                typeof(string),
                typeof(SimpleProjectView), 
                new PropertyMetadata(default(string))
            );

        public string ProjectName
        {
            get => (string) GetValue(ProjectNameProperty);
            set => SetValue(ProjectNameProperty, value);
        }

        public SimpleProjectView()
        {
            InitializeComponent();
        }
    }
}
