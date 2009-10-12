using System;
using System.Windows;
using System.Windows.Controls;

namespace Balder.Silverlight.Helpers
{
	public static class DataContextHelper
	{
		public static object GetFirstDataContextInVisualTree()
		{
			return GetFirstDataContextInVisualTree(Application.Current.RootVisual as FrameworkElement);
		}

		public static object GetFirstDataContextInVisualTree(FrameworkElement root)
		{
			if (null != root.DataContext)
			{
				return root.DataContext;
			}

			Func<UIElementCollection,object> walkChildren =
				(c) =>
				{
					foreach (FrameworkElement child in c)
					{
						var returnValue = GetFirstDataContextInVisualTree(child);
						if (null != returnValue)
						{
							return returnValue;
						}
					}
					return null;
				};


			object result = null;
			if (root is Panel)
			{
				result = walkChildren(((Panel) root).Children);
			}
			else if( root is Grid )
			{
				result = walkChildren(((Grid) root).Children);
			} else if( root is ContentControl )
			{
				result = GetFirstDataContextInVisualTree((FrameworkElement)((ContentControl)root).Content);
			} else
			{
				var layoutRoot = root.FindName("LayoutRoot") as FrameworkElement;
				if( null != layoutRoot )
				{
					result = GetFirstDataContextInVisualTree(layoutRoot);
				}
			}
			return result;
		}

	}
}
