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
        public static readonly DependencyProperty FirstButtonCommandProperty = DependencyProperty.Register(
            nameof(FirstButtonCommand), typeof(ICommand), typeof(CustomViewDataButtonsGrid), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty SecondButtonCommandProperty = DependencyProperty.Register(
            nameof(SecondButtonCommand), typeof(ICommand), typeof(CustomViewDataButtonsGrid), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty FirstButtonCommandParameterProperty = DependencyProperty.Register(
            nameof(FirstButtonCommandParameter), typeof(object), typeof(CustomViewDataButtonsGrid), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SecondButtonCommandParameterProperty = DependencyProperty.Register(
            nameof(SecondButtonCommandParameter), typeof(object), typeof(CustomViewDataButtonsGrid), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty FirstButtonContentProperty = DependencyProperty.Register(
            nameof(FirstButtonContent), typeof(object), typeof(CustomViewDataButtonsGrid), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SecondButtonContentProperty = DependencyProperty.Register(
            nameof(SecondButtonContent), typeof(object), typeof(CustomViewDataButtonsGrid), new PropertyMetadata(default(object)));

        public ICommand FirstButtonCommand
        {
            get => (ICommand)GetValue(FirstButtonCommandProperty);
            set => SetValue(FirstButtonCommandProperty, value);
        }

        public ICommand SecondButtonCommand
        {
            get => (ICommand) GetValue(SecondButtonCommandProperty);
            set => SetValue(SecondButtonCommandProperty, value);
        }

        public object FirstButtonCommandParameter
        {
            get => (object) GetValue(FirstButtonCommandParameterProperty);
            set => SetValue(FirstButtonCommandParameterProperty, value);
        }

        public object SecondButtonCommandParameter
        {
            get => (object) GetValue(SecondButtonCommandParameterProperty);
            set => SetValue(SecondButtonCommandParameterProperty, value);
        }

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

        private void FirstButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is NavButton { Name: "ViewButton" } clickedButton)
            {
                var projectId = (int)clickedButton.CommandParameter;
                //NavigationService!.Navigate(clickedButton!.NavButtonUri);
            }
        }
    }
}
