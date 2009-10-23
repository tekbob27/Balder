using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Balder.Core.Interfaces;
using Balder.Core.Services;
using Balder.Core.SoftwareRendering;
using Balder.Silverlight.Implementation;
using System.Threading;
using Balder.Silverlight.SoftwareRendering;

namespace Balder.Silverlight.Services
{
	public delegate void DrawHandler(EventWaitHandle waitHandle);

	public class Display : Canvas, IDisplay
	{
		private IBuffers _buffers;
		private Image _image;
		private BitmapImage _imageSource;
		private Color _backgroundColor;

		private readonly FrameworkElement _root;

		

		public Display()
		{
			HorizontalAlignment = HorizontalAlignment.Left;
			VerticalAlignment = VerticalAlignment.Top;

			BackgroundColor = Colors.Black;

			_root = GetRoot();
			AddDisplayToRoot();

			var parentWithDimensionsSet = FindParentWithDimensionsSet(_root);
			if( null != parentWithDimensionsSet )
			{
				Initialize();
			} else
			{
				Loaded += DisplayLoaded;	
			}
		}


		#region Visual Tree stuff
		private static T FindFirst<T>()
			where T : UIElement
		{
			return FindFirst<T>(Application.Current.RootVisual);

		}

		private static T FindFirst<T>(UIElement element)
			where T : UIElement
		{
			if (null == element)
			{
				return null;
			}
			if (element is ContentControl)
			{
				return FindFirst<T>(((ContentControl)element).Content as UIElement);
			}
			if (element is T)
			{
				return element as T;
			}
			var child = VisualTreeHelper.GetChild(element, 0);
			if (null != child && child is UIElement)
			{
				return FindFirst<T>(child as UIElement);
			}
			return null;
		}

		private FrameworkElement GetRoot()
		{
			var panelRoot = FindFirst<Panel>();
			if (null != panelRoot)
			{
				return panelRoot;
			}
			else
			{
				var root = FindFirst<ItemsControl>();
				if (null != root)
				{
					return root;
				}
			}

			return null;
		}


		private void AddDisplayToRoot()
		{
			if (null != _root)
			{
				if (_root is Panel)
				{
					((Panel)_root).Children.Add(this);
				}
				else if (_root is ItemsControl)
				{
					((ItemsControl)_root).Items.Add(this);
				}
			}
		}

		private static FrameworkElement FindParentWithDimensionsSet(FrameworkElement element)
		{
			if( !element.Width.Equals(Double.NaN) && 
				element.Width != 0 && 
				!element.Height.Equals(Double.NaN) && 
				element.Height != 0)
			{
				return element;
			}

			if( null != element.Parent && element.Parent is FrameworkElement )
			{
				return FindParentWithDimensionsSet(element.Parent as FrameworkElement);	
			}
			return null;
		}


		private void AutomaticallyAdjustDimensions()
		{
			if( null != _root )
			{
				var elementWithDimensions = FindParentWithDimensionsSet(_root);
				if( null != elementWithDimensions )
				{
					Width = elementWithDimensions.Width;
					Height = elementWithDimensions.Height;
				}
			}
		}
		#endregion


		private void Initialize()
		{
			AutomaticallyAdjustDimensions();
			_image = new Image { Stretch = Stretch.None };

			_buffers = BufferManager.Instance.Create<FrameBuffer>((int)Width, (int)Height);

			_image.Source = _buffers.FrameBuffer.BitmapSource;
			_buffers.FrameBuffer.Render += OnDraw;
			_buffers.FrameBuffer.Updated += OnUpdate;
			BufferManager.Instance.Current = _buffers;
			_buffers.Start();

			_image.Width = Width;
			_image.Height = Height;


			Children.Add(_image);
		}

		private void DisplayLoaded(object sender, RoutedEventArgs e)
		{
			Initialize();
		}

		public void Stop()
		{
			_buffers.Stop();
		}

		public Color BackgroundColor
		{
			get
			{
				return _backgroundColor;
			}
			set
			{
				_backgroundColor = value;
				Background = new SolidColorBrush(value);
			}
		}


		public event EventHandler Draw = (s, e) => { };
		public event EventHandler Render = (s, e) => { };
		public event EventHandler Update = (s, e) => { };

		public IViewport CreateViewport(int xpos, int ypos, int width, int height)
		{
			var viewport = new Viewport
							{
								XPosition = xpos,
								YPosition = ypos,
								Width = width,
								Height = height
							};
			Children.Add(viewport);
			return viewport;
		}


		private void OnDraw()
		{
			Render(this, null);
			Draw(this, null);
		}

		private void OnUpdate()
		{
			Update(this, null);
		}
	}
}
