using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using Balder.Silverlight.Helpers;
using DependencyProperty = System.Windows.DependencyProperty;

namespace Balder.Silverlight.Extensions
{
	public interface IDependencyPropertySubscription : INotifyPropertyChanged
	{
		object Value { get; set; }
	}

	public class DependencyPropertySubscription<T> : FrameworkElement, IDependencyPropertySubscription
		where T:FrameworkElement
	{
		public T Element { get; private set; }
		public DependencyProperty DependencyProperty { get; private set; }
		private DependencyProperty _hiddenAttachedProperty;
		public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

		public DependencyPropertySubscription(T element, DependencyProperty dependencyProperty)
		{
			this.Element = element;
			this.DependencyProperty = dependencyProperty;

			var sourceBinding = new System.Windows.Data.Binding("Value") { Source = this, Mode = BindingMode.TwoWay };
			element.SetBinding(this.DependencyProperty, sourceBinding);
		}


		private static readonly DependencyProperty<DependencyPropertySubscription<T>, object> ValueProperty =
			DependencyProperty<DependencyPropertySubscription<T>, object>.Register(o => o.Value);
		public object Value
		{
			get { return ValueProperty.GetValue(this); }
			set
			{
				ValueProperty.SetValue(this, value);
				this.PropertyChanged.Notify(()=>Value);
			}
		}
	}
}