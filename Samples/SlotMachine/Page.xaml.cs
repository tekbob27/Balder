using System;
using System.Windows;
using System.Windows.Controls;
using Balder.Core;
using SlotMachine.Objects;

namespace SlotMachine
{
	public partial class Page : UserControl
	{
		public Page()
		{
			InitializeComponent();

			this.Loaded += new RoutedEventHandler(Page_Loaded);
		}

		public void Page_Loaded(object o, EventArgs e)
		{
			// Required to initialize variables
			InitializeComponent();

			this._mainCanvas_FadeIn.Begin();
			this._loadCanvas_FadeOut.Begin();
		}

		private void Application_Loaded(object sender, EventArgs e)
		{
			Balder.Core.Application application = (Balder.Core.Application)this.FindName("application");
			if (null != application)
			{
				this.application.Initialize(this, this._rootCanvas);

				application.Viewport = new Viewport(640, 480);
				application.Viewport.Scene = new Scene();

				Slot slot = new Slot(this._rootCanvas, application);

				ScoreManager.Instance.RootCanvas = this._rootCanvas;
				//SymbolManager.Instance.CheckScore(null);
			}
		}

		private void Application_Run(object sender, EventArgs e)
		{
			if (null == this.application)
			{
				return;
			}

			// Center the text for the scoreboard!!
			if (null != this._scoreBoardText &&
				null != this._scoreBoardCanvas &&
				null != this._stateInfoText)
			{
				double left = (this._scoreBoardCanvas.Width - this._scoreBoardText.ActualWidth) / 2;
				this._scoreBoardText.SetValue(Canvas.LeftProperty, left);
				left = (this._scoreBoardCanvas.Width - this._stateInfoText.ActualWidth) / 2;
				this._stateInfoText.SetValue(Canvas.LeftProperty, left);
			}

			this.HandleBackground();
		}


		#region Background
		private double _backgroundLayerASin = 0.0;
		private double _backgroundLayerBSin = 0.0;
		private double _backgroundLayerCSin = 0.0;

		private void HandleBackground()
		{
			double layerA_XRadius = (this._backgroundLayerA.Width - this.Width) / 3;
			double layerA_YRadius = (this._backgroundLayerA.Height - this.Height) / 3;

			this._backgroundLayerAPosition.X = (Math.Sin(this._backgroundLayerASin) *
												layerA_XRadius) + layerA_XRadius;
			this._backgroundLayerAPosition.Y = (Math.Cos(this._backgroundLayerASin) *
												layerA_YRadius) + layerA_YRadius;


			double layerB_XRadius = (this._backgroundLayerB.Width - this.Width) / 3;
			double layerB_YRadius = (this._backgroundLayerB.Height - this.Height) / 3;

			this._backgroundLayerBPosition.X = (Math.Sin(this._backgroundLayerBSin) *
												layerB_XRadius) + layerB_XRadius;
			this._backgroundLayerBPosition.Y = (Math.Cos(this._backgroundLayerBSin) *
												layerB_YRadius) + layerB_YRadius;

			double layerC_XRadius = (this._backgroundLayerC.Width - this.Width) / 3;
			double layerC_YRadius = (this._backgroundLayerC.Height - this.Height) / 3;

			this._backgroundLayerCPosition.X = (Math.Sin(this._backgroundLayerCSin) *
												layerC_XRadius) + layerC_XRadius;
			this._backgroundLayerCPosition.Y = (Math.Cos(this._backgroundLayerCSin) *
												layerC_YRadius) + layerC_YRadius;


			this._backgroundLayerASin += 0.013;
			this._backgroundLayerARotation.Angle -= 0.2;

			this._backgroundLayerBSin -= 0.02;
			this._backgroundLayerBRotation.Angle += 0.32;

			this._backgroundLayerCSin -= 0.01;
			this._backgroundLayerCRotation.Angle -= 0.17;
		}
		#endregion
	}
}
