using System;
using Balder.Silverlight.Helpers;

namespace Balder.Silverlight.Controls
{
	public class Game : BalderControl
	{
		public Game()
		{
			Loaded += GameLoaded;
		}

		private void GameLoaded(object sender, EventArgs e)
		{
			Validate();
			if( null != GameClass )
			{
				InitializeGame();
			}
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
			var display = Platform.DisplayDevice.CreateDisplay();
			display.Initialize((int)Width,(int)Height);
			Runtime.RegisterGame(display, GameClass);
		}


		public DependencyProperty<Game, Core.Execution.Game> GameClassProperty =
			DependencyProperty<Game, Core.Execution.Game>.Register(g => g.GameClass);
		public Core.Execution.Game GameClass
		{
			get { return GameClassProperty.GetValue(this); }
			set { GameClassProperty.SetValue(this,value); }
		}
	}
}
