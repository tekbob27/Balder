using System;
using System.Windows;
using System.Windows.Controls;
using Balder.Core;
using Balder.Core.Implementation;
using Balder.Core.Runtime;
using Balder.Core.Services;
using Balder.Silverlight.Services;
using Balder.Core.Interfaces;

namespace Balder.Silverlight.Controls
{
	public class BalderDisplay : Panel
	{
		public event EventHandler LoadingContent = (s, e) => { };
		public event EventHandler Updating = (s, e) => { };

		public class BalderDisplayInternalGame : Game
		{
			public event EventHandler LoadContentOccured = (s, e) => { };

			public override void LoadContent()
			{
				LoadContentOccured(this, null);
				base.LoadContent();
			}
		}

		public BalderDisplay()
		{
			Loaded += BalderDisplay_Loaded;
			
		}

		void BalderDisplay_Loaded(object sender, RoutedEventArgs e)
		{
			Initialize();
		}

		private void Initialize()
		{
			
			Balder.Silverlight.Services.Display.RootElement = this;
			TargetDevice.Initialize<BalderDisplayInternalGame>(g =>
			                                                   	{
			                                                   		if (null != g && g is BalderDisplayInternalGame)
			                                                   		{
			                                                   			Game = g;
			                                                   			SetupEvents(g as BalderDisplayInternalGame);
			                                                   		}
			                                                   	});

			
		}
		
		private void SetupEvents(BalderDisplayInternalGame game)
		{
			game.LoadContentOccured += (s, e) => LoadingContent(s, e);
			game.Updating += (s, e) => Updating(s, e);
		}

		private Game Game { get; set; }

		public IContentManager ContentManager { get { return Game.ContentManager;  } }
		public IDisplay Display { get { return Game.Display; } }
		public Scene Scene { get { return Game.Scene;  } }
		public Camera Camera { get { return Game.Camera; } }
		public IViewport Viewport { get { return Game.Viewport; } } 
	}
}
