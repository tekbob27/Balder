using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Balder.Core;
using Balder.Core.Interfaces;

namespace Balder.Silverlight.Implementation
{
	public class Viewport : Canvas, IViewport
	{
		private static readonly SolidColorBrush DebugLineBrush = new SolidColorBrush(Colors.White);

		#region Private fields
		private int _xPosition;
		private int _yPosition;
		private int _width;
		private int _height;
		#endregion

		public Viewport()
		{
			HorizontalAlignment = HorizontalAlignment.Left;
			VerticalAlignment = VerticalAlignment.Top;
		}

		#region Public properties

		public int XPosition
		{
			get { return _xPosition; }
			set
			{
				_xPosition = value;
				SetLeft(this, value);
			}
		}

		public int YPosition
		{
			get { return _yPosition; }
			set
			{
				_yPosition = value;
				SetTop(this, value);
			}
		}

		public new int Width
		{
			get { return _width; }
			set
			{
				_width = value;
				base.Width = value;
			}
		}

		public new int Height
		{
			get { return _height; }
			set
			{
				_height = value;
				base.Height = value;
			}
		}

		public Scene Scene { get; set; }
		public Camera Camera { get; set; }

		#endregion


		public void Prepare()
		{
			if (null == Scene)
			{
				throw new ArgumentException("Scene must be defined first");
			}

			var clipping = new RectangleGeometry { Rect = new Rect(0, 0, Width, Height) };
			Clip = clipping;

		}

		public void BeforeRender()
		{
		}

		public void AfterRender()
		{
		}
	}
}
