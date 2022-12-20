using System;
using System.Windows;
using System.Windows.Media;

namespace TaskManagerWPF.Services.Misc;

public static class UIChildFinder
{
    public static DependencyObject FindChild(this DependencyObject reference, string childName, Type childType)
    {
        DependencyObject foundChild = null;
        if (reference != null)
        {
            int childrenCount = VisualTreeHelper.GetChildrenCount(reference);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(reference, i);
                // If the child is not of the request child type child
                if (child.GetType() != childType)
                {
                    // recursively drill down the tree
                    foundChild = FindChild(child, childName, childType);
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = child;
                    break;
                }
            }
        }
        return foundChild;
    }
}