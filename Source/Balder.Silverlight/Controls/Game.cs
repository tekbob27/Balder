using System;
using System.Windows.Controls;
using System.Windows.Media;
using Balder.Core.Display;
using Balder.Silverlight.Helpers;
using Balder.Silverlight.Input;
using Balder.Silverlight.SoftwareRendering;

namespace Balder.Silverlight.Controls
{
	
	public class Game : BalderControl
	{
		private Image _image;
		private Color _previousBackgroundColor;


		public Game()
		{
			Loaded += GameLoaded;

			RenderingManager.Instance.Updated += RenderingManagerUpdated;
		}

		private void RenderingManagerUpdated()
		{
			if (!_previousBackgroundColor.Equals(Display.BackgroundColor))
			{
				SetBackgroundColor();
			}
		}

		private void SetBackgroundColor()
		{
			var color = Display.BackgroundColor.ToSystemColor();
			Background = new SolidColorBrush(color);
			_previousBackgroundColor = color;
		}

		private void GameLoaded(object sender, EventArgs e)
		{
			Validate();
			/*
			if (null != GameClass)
			{
				InitializeGame();
			}
			InitializeContent();*/
		}

		private void Validate()
		{
			if (0 == Width || Width.Equals(double.NaN) ||
				0 == Height || Height.Equals(double.NaN))
			{
				throw new ArgumentException("You need to specify Width and Height");
			}
		}

		private void InitializeGame()
		{
			Display = Platform.DisplayDevice.CreateDisplay();
			Display.Initialize((int)Width, (int)Height);
			Runtime.RegisterGame(Display, GameClass);

			if (Platform.MouseDevice is MouseDevice)
			{
				((MouseDevice)Platform.MouseDevice).Initialize(this);
			}
		}

		private void InitializeContent()
		{
			if (Display is Display.Display)
			{
				_image = new Image
							{
								Source = ((Display.Display)Display).FramebufferBitmap,
								Stretch = Stretch.None
							};
				Children.Add(_image);

				SetBackgroundColor();
			}

			Camera = new Camera();
			AddNodesToScene();
		}

		private void AddNodesToScene()
		{
			foreach( var element in Children )
			{
				if( element is Node && !(element is Camera) )
				{
					var node = element as Node;
					if (null != node.ActualNode)
					{
						GameClass.Scene.AddNode(node.ActualNode);
					}
				}
			}
			
		}

		public IDisplay Display { get; private set; }

		public DependencyProperty<Game, Core.Execution.Game> GameClassProperty =
			DependencyProperty<Game, Core.Execution.Game>.Register(g => g.GameClass);
		public Core.Execution.Game GameClass
		{
			get { return GameClassProperty.GetValue(this); }
			set { GameClassProperty.SetValue(this, value); }
		}


		public DependencyProperty<Game, Camera> CameraProperty =
			DependencyProperty<Game, Camera>.Register(g => g.Camera);
		public Camera Camera
		{
			get
			{
				var camera = CameraProperty.GetValue(this);
				if( null == camera )
				{
					camera = new Camera();
				}

				return camera;
			}
			set
			{
				/*
				var previousCamera = value;
				if( null != previousCamera )
				{
					Children.Remove(previousCamera);
				}
				 * */
				CameraProperty.SetValue(this, value);
				//Children.Add(value);
			}
		}


	}
}
