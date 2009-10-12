using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Balder.Core.Math;

namespace Coverflow
{
	public partial class CoverFlow : UserControl
	{
		public static DependencyProperty CoverProperty =
			DependencyProperty.Register("Cover", typeof(double), typeof(CoverFlow), new PropertyMetadata(0d, new PropertyChangedCallback(OnCoverChanged)));


		public const double CameraYPos = 0;
		public const double CameraZPos = 100;

		public const double CoverSpace = 5d;
		public const double CoverChangeDuration = 500d;
		public const double CurrentCoverOpeningSpace = 5d;

		private List<Cover> _covers;
		private int _currentCoverIndex;

		private Storyboard _coverStoryboard;
		private DoubleAnimation _coverAnimation;


		public CoverFlow()
		{
			InitializeComponent();
			this._covers = new List<Cover>();

			this._camera.Position = new Vector(0, CameraYPos, CameraZPos);

			this._coverStoryboard = new Storyboard();
			this._coverAnimation = new DoubleAnimation();
			this._coverAnimation.Duration = TimeSpan.FromMilliseconds(CoverChangeDuration);
			this._coverStoryboard.Children.Add(this._coverAnimation);
			Storyboard.SetTarget(this._coverAnimation, this);
			Storyboard.SetTargetProperty(this._coverAnimation, new PropertyPath("(CoverFlow.Cover)"));
		}

		public void AddCover()
		{
			double coverPos = ((double)this._covers.Count) * CoverSpace;
			Cover cover = new Cover();
			this._covers.Add(cover);
			this._viewport.Children.Add(cover);

			cover.Position = new Vector(coverPos, 0, 0);
			this.Update();

		}

		public void Update()
		{
			int index = 0;
			foreach (var cover in this._covers)
			{
				if (index == this._currentCoverIndex)
				{
					cover.MoveToFront();
				}
				else if (index < this._currentCoverIndex)
				{
					cover.MoveToLeft();
				}
				else
				{
					cover.MoveToRight();
				}

				index++;
			}
		}

		private int CurrentCoverIndex
		{
			get
			{
				return this._currentCoverIndex;
			}
			set
			{
				this._currentCoverIndex = value;
				this.Update();
			}
		}

		public double Cover
		{
			get
			{
				return (double)this.GetValue(CoverProperty);
			}
			set
			{
				this.SetValue(CoverProperty, value);
			}
		}



		public static void OnCoverChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			CoverFlow coverFlow = (CoverFlow)obj;


			coverFlow.CurrentCoverIndex = Convert.ToInt32(e.NewValue);

			var currentCover = ((double)e.NewValue);





			double xpos = ((double)e.NewValue) * CoverSpace;

			coverFlow._camera.Target = new Vector(xpos, 0, 0);
			coverFlow._camera.Position = new Vector(xpos, CameraYPos, CameraZPos);

			coverFlow.Cover = (double)e.NewValue;

		}

		public int CurrentlyBeingMovedTo
		{
			get; private set;
		}

		public void MoveTo(int cover)
		{
			this.CurrentlyBeingMovedTo = cover;
			this._coverAnimation.To = (double)cover;
			this._coverStoryboard.Begin();
		}

		public void MoveNext()
		{
			int next = this.CurrentlyBeingMovedTo + 1;
			if( next >= this._covers.Count )
			{
				next = this._covers.Count - 1;
			}
			this.MoveTo(next);
		}

		public void MovePrevious()
		{
			int next = this.CurrentlyBeingMovedTo - 1;
			if( next < 0 )
			{
				next = 0;
			}
			this.MoveTo(next);
		}
	}
}
