using System.ComponentModel;
using System.Windows;
using Balder.Silverlight.Extensions;
using Balder.Silverlight.Helpers;

namespace Balder.Silverlight.Controls.Math
{
	public class Vector : DependencyObject, INotifyPropertyChanged
	{
		public static readonly DependencyProperty<Vector, double> XProperty =
			DependencyProperty<Vector, double>.Register(o => o.X);
		public double X
		{
			get { return XProperty.GetValue(this); }
			set
			{
				XProperty.SetValue(this, value);
				PropertyChanged.Notify(() => X);
			}
		}

		public static readonly DependencyProperty<Vector, double> YProperty =
			DependencyProperty<Vector, double>.Register(o => o.Y);
		public double Y
		{
			get { return YProperty.GetValue(this); }
			set
			{
				YProperty.SetValue(this, value);
				PropertyChanged.Notify(() => Y);

			}
		}

		public static readonly DependencyProperty<Vector, double> ZProperty =
			DependencyProperty<Vector, double>.Register(o => o.Z);
		public double Z
		{
			get { return ZProperty.GetValue(this); }
			set
			{
				ZProperty.SetValue(this, value);
				PropertyChanged.Notify(() => Z);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
	}
}
