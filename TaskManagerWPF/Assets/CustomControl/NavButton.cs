using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TaskManagerWPF.Assets.CustomControl
{
    public class NavButton : Button
    {
        static NavButton()
        {
        }

        public static readonly DependencyProperty NavButtonContentProperty = DependencyProperty.Register(
            nameof(NavButtonContent),
            typeof(object),
            typeof(NavButton),
            new PropertyMetadata(default(object))
        );

        public static readonly DependencyProperty NavButtonUriProperty = DependencyProperty.Register(
            nameof(NavButtonUri),
            typeof(Uri),
            typeof(NavButton),
            new PropertyMetadata(default(Uri))
        );

        public object NavButtonContent
        {
            get => (object)GetValue(NavButtonContentProperty);
            set => SetValue(NavButtonContentProperty, value);
        }

        public Uri NavButtonUri
        {
            get => (Uri)GetValue(NavButtonUriProperty);
            set => SetValue(NavButtonUriProperty, value);
        }
    }
}
