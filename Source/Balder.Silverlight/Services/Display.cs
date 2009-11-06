using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Balder.Core.Display;
using Balder.Core.Interfaces;
using Balder.Core.Runtime;
using Balder.Core.SoftwareRendering;
using Balder.Silverlight.Implementation;
using System.Threading;
using Balder.Silverlight.SoftwareRendering;

namespace Balder.Silverlight.Services
{
	public delegate void DrawHandler(EventWaitHandle waitHandle);

	public class Display : Canvas, IDisplay
	{
		private static readonly EventArgs DefaultEventArgs = new EventArgs();

		public event EventHandler Draw = (s, e) => { };
		public event EventHandler Render = (s, e) => { };
		public event EventHandler Update = (s, e) => { };
		public event EventHandler Initialized = (s, e) => { };


		private IBuffers _buffers;
		private Image _image;
		private BitmapImage _imageSource;
		private Color _backgroundColor;

		private FrameworkElement _root;

		public Display()
		{
			HorizontalAlignment = HorizontalAlignment.Left;
			VerticalAlignment = VerticalAlignment.Top;

			BackgroundColor = Colors.Black;
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

					((Panel)_root).Children.Insert(0, this);
				}
				else if (_root is ItemsControl)
				{
					((ItemsControl)_root).Items.Insert(0, this);
				}
			}
		}

		private static FrameworkElement FindParentWithDimensionsSet(FrameworkElement element)
		{
			if (!element.Width.Equals(Double.NaN) &&
				element.Width != 0 &&
				!element.Height.Equals(Double.NaN) &&
				element.Height != 0)
			{
				return element;
			}

			if (null != element.Parent && element.Parent is FrameworkElement)
			{
				return FindParentWithDimensionsSet(element.Parent as FrameworkElement);
			}
			return null;
		}


		private void AutomaticallyAdjustDimensions()
		{
			if (null != _root)
			{
				var elementWithDimensions = FindParentWithDimensionsSet(_root);
				if (null != elementWithDimensions)
				{
					Width = elementWithDimensions.Width;
					Height = elementWithDimensions.Height;
				}
			}
		}
		#endregion

		public void Initialize()
		{
			Initialize(null);
		}

		public void Initialize(FrameworkElement root)
		{
			if (null == root)
			{
				_root = GetRoot();
				AddDisplayToRoot();
			}
			else
			{
				_root = root;
			}

			AutomaticallyAdjustDimensions();
			_image = new Image { Stretch = Stretch.None };

			Initialized(this, DefaultEventArgs);

			_buffers = BufferManager.Instance.Create<FrameBuffer>((int)Width, (int)Height);

			_image.Source = _buffers.FrameBuffer.BitmapSource;
			_buffers.FrameBuffer.Render += OnDraw;
			_buffers.FrameBuffer.Updated += OnUpdate;
			BufferManager.Instance.Current = _buffers;

			_image.Width = Width;
			_image.Height = Height;

			Children.Add(_image);

			IsInitialized = true;
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


		public bool IsInitialized { get; private set; }

		public IViewport CreateViewport()
		{
			var viewport = new Viewport();

			if (0 != Width && !Width.Equals(double.NaN) &&
				0 != Height && !Height.Equals(double.NaN))
			{
				viewport.Width = (int)Width;
				viewport.Height = (int)Height;
			}
			Children.Add(viewport);
			return viewport;
		}

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
			Render(this, DefaultEventArgs);
			Draw(this, DefaultEventArgs);
		}

		private void OnUpdate()
		{
			Update(this, DefaultEventArgs);
		}
	}
}
