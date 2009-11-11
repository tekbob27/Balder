using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Balder.Core.Display;
using Balder.Silverlight.Helpers;
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
			if( !_previousBackgroundColor.Equals(Display.BackgroundColor))
			{
				SetBackgroundColor();
			}
		}

		private void SetBackgroundColor()
		{
			Background = new SolidColorBrush(Display.BackgroundColor);
			_previousBackgroundColor = Display.BackgroundColor;
		}

		private void GameLoaded(object sender, EventArgs e)
		{
			Validate();
			if( null != GameClass )
			{
				InitializeGame();
			}
			InitializeContent();
		}

		private void Validate()
		{
			if( 0 == Width || Width.Equals(double.NaN) ||
				0 == Height || Height.Equals(double.NaN) )
			{
				throw new ArgumentException("You need to specify Width and Height");
			}
		}

		private void InitializeGame()
		{
			Display = Platform.DisplayDevice.CreateDisplay();
			Display.Initialize((int)Width,(int)Height);
			Runtime.RegisterGame(Display, GameClass);
		}

		private void InitializeContent()
		{
			if( Display is Display.Display )
			{
				_image = new Image
				         	{
				         		Source = ((Display.Display) Display).FramebufferBitmap,
				         		Stretch = Stretch.None
				         	};
				Children.Add(_image);

				SetBackgroundColor();
			}
		}

		public IDisplay Display { get; private set; }

		public DependencyProperty<Game, Core.Execution.Game> GameClassProperty =
			DependencyProperty<Game, Core.Execution.Game>.Register(g => g.GameClass);
		public Core.Execution.Game GameClass
		{
			get { return GameClassProperty.GetValue(this); }
			set { GameClassProperty.SetValue(this,value); }
		}
	}
}
