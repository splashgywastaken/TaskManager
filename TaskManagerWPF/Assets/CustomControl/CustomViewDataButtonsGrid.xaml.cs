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

namespace TaskManagerWPF.Assets.CustomControl
{
    /// <summary>
    /// Interaction logic for CustomViewDataButtonsGrid.xaml
    /// </summary>
    public partial class CustomViewDataButtonsGrid : UserControl
    {
        public static readonly DependencyProperty FirstButtonContentProperty = DependencyProperty.Register(
            nameof(FirstButtonContent), typeof(object), typeof(CustomViewDataButtonsGrid), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SecondButtonContentProperty = DependencyProperty.Register(
            nameof(SecondButtonContent), typeof(object), typeof(CustomViewDataButtonsGrid), new PropertyMetadata(default(object)));

        public object FirstButtonContent
        {
            get => (object) GetValue(FirstButtonContentProperty);
            set => SetValue(FirstButtonContentProperty, value);
        }

        public object SecondButtonContent
        {
            get => (object) GetValue(SecondButtonContentProperty);
            set => SetValue(SecondButtonContentProperty, value);
        }

        public CustomViewDataButtonsGrid()
        {
            InitializeComponent();
        }
    }
}
