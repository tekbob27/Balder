using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Media;
using Balder.Core;
using Balder.Core.Display;
using Balder.Core.Runtime;
using Balder.Silverlight.Helpers;
using Balder.Silverlight.Services;

namespace Balder.Silverlight.Controls
{
	public delegate void RenderingContainerEventHandler(RenderingContainer container);

	public class RenderingContainer : ContentControl
	{
		public event RenderingContainerEventHandler Updated = (c) => { };


		public class RenderingContainerGame : Game
		{
		}

		public RenderingContainer()
		{
			Loaded += RenderingContainer_Loaded;
		}


		private void HandleNodes(IEnumerable nodes)
		{
			foreach (RenderedNode node in nodes)
			{
				node.Initialize(Game.ContentManager, Game.Scene);
			}
		}

		private void RenderingContainer_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if (Width.Equals(Double.NaN) ||
				Width == 0 ||
				Height.Equals(Double.NaN) ||
				Height == 0)
			{
				throw new ArgumentException("RenderingContainer must have its dimensions set (Width, Height)");
			}

			Display = EngineRuntime.Instance.CreateDisplay();
			Game = EngineRuntime.Instance.RegisterGame<RenderingContainerGame>(Display);

			Game.Updated += (g) => Updated(this);

			var display = Display as Display;
			Content = display;

			display.Initialize(this);
			InitializeProperties();
		}

		private void InitializeProperties()
		{
			Display.BackgroundColor = BackgroundColor;
			HandleNodes(Nodes.Items);
		}

		public static DependencyProperty<RenderingContainer, Color> BackgroundColorProperty =
			DependencyProperty<RenderingContainer, Color>.Register(o => o.BackgroundColor);
		public Color BackgroundColor
		{
			get { return BackgroundColorProperty.GetValue(this); }
			set
			{
				BackgroundColorProperty.SetValue(this, value);
				if (null != Display)
				{
					Display.BackgroundColor = value;
				}
			}
		}

		public static DependencyProperty<RenderingContainer, RenderedNodeCollection> NodesProperty =
			DependencyProperty<RenderingContainer, RenderedNodeCollection>.Register(o => o.Nodes);
		public RenderedNodeCollection Nodes
		{
			get { return NodesProperty.GetValue(this); }
			set 
			{ 
				NodesProperty.SetValue(this,value); 
				if( null != Game )
				{
					HandleNodes(value.Items);
				}
			}
		}

		public IDisplay Display { get; private set; }
		public Game Game { get; private set; }
		public Camera Camera { get { return Game.Camera;  } }
	}
}
