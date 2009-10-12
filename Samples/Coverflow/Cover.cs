using System;
using System.Windows;
using System.Windows.Media.Animation;
using Balder.Core;
using Balder.Core.GeometryArtifacts;
using Balder.Core.Materials;

namespace Coverflow
{
	public class Cover : Geometry
	{
		public const double CoverWidth = 10d;
		public const double CoverHeight = 10d;
		public const double CoverAngle = 85d;
		public const double CoverMoveDuration = 100d;
		public const double CoverOffset = 10d;

		private Storyboard _moveToLeftStoryboard;
		private Storyboard _moveToRightStoryboard;
		private Storyboard _moveToFrontStoryboard;

		public Cover()
		{
			this.AddVertex(-CoverWidth, -CoverHeight, 0);
			this.AddVertex(CoverWidth, -CoverWidth, 0);
			this.AddVertex(-CoverWidth, CoverHeight, 0);
			this.AddVertex(CoverWidth, CoverWidth, 0);


			this.AddTextureCoordinate(0d, 0d);
			this.AddTextureCoordinate(1d, 0d);
			this.AddTextureCoordinate(0d, 1d);
			this.AddTextureCoordinate(1d, 1d);

			Material material = new Material(new Uri("Empty.png",UriKind.Relative));
			
			Face face1 = this.AddFace(0, 1, 2, 0, 1, 2);
			face1.DoubleSided = true;
			Face face2 = this.AddFace(3, 2, 1, 3, 2, 1);
			face2.DoubleSided = true;
			face1.Material = material;
			face2.Material = material;


			// Move left
			this._moveToLeftStoryboard = new Storyboard();
			var moveToLeftAnimation = new DoubleAnimation();
			moveToLeftAnimation.To = CoverAngle;
			moveToLeftAnimation.Duration = TimeSpan.FromMilliseconds(CoverMoveDuration);
			this._moveToLeftStoryboard.Children.Add(moveToLeftAnimation);
			Storyboard.SetTarget(moveToLeftAnimation, this);
			Storyboard.SetTargetProperty(moveToLeftAnimation, new PropertyPath("(Node.YRotation)"));

			var offsetToLeftAnimation = new DoubleAnimation();
			offsetToLeftAnimation.To = -CoverOffset;
			offsetToLeftAnimation.Duration = TimeSpan.FromMilliseconds(CoverMoveDuration);
			this._moveToLeftStoryboard.Children.Add(offsetToLeftAnimation);
			Storyboard.SetTarget(offsetToLeftAnimation, this);
			Storyboard.SetTargetProperty(offsetToLeftAnimation, new PropertyPath("(Node.XPosition)"));

			var moveLeftBackwardAnimation = new DoubleAnimation();
			moveLeftBackwardAnimation.To = 0;
			moveLeftBackwardAnimation.Duration = TimeSpan.FromMilliseconds(CoverMoveDuration);
			this._moveToLeftStoryboard.Children.Add(moveLeftBackwardAnimation);
			Storyboard.SetTarget(moveLeftBackwardAnimation, this);
			Storyboard.SetTargetProperty(moveLeftBackwardAnimation, new PropertyPath("(Node.ZPosition)"));


			// Move right
			this._moveToRightStoryboard = new Storyboard();
			var moveToRightAnimation = new DoubleAnimation();
			moveToRightAnimation.To = -CoverAngle;
			moveToRightAnimation.Duration = TimeSpan.FromMilliseconds(CoverMoveDuration);
			this._moveToRightStoryboard.Children.Add(moveToRightAnimation);
			Storyboard.SetTarget(moveToRightAnimation, this);
			Storyboard.SetTargetProperty(moveToRightAnimation, new PropertyPath("(Node.YRotation)"));

			var offsetToRightAnimation = new DoubleAnimation();
			offsetToRightAnimation.To = CoverOffset;
			offsetToRightAnimation.Duration = TimeSpan.FromMilliseconds(CoverMoveDuration);
			this._moveToRightStoryboard.Children.Add(offsetToRightAnimation);
			Storyboard.SetTarget(offsetToRightAnimation, this);
			Storyboard.SetTargetProperty(offsetToRightAnimation, new PropertyPath("(Node.XPosition)"));

			var moveRightBackwardAnimation = new DoubleAnimation();
			moveRightBackwardAnimation.To = 0;
			moveRightBackwardAnimation.Duration = TimeSpan.FromMilliseconds(CoverMoveDuration);
			this._moveToRightStoryboard.Children.Add(moveRightBackwardAnimation);
			Storyboard.SetTarget(moveRightBackwardAnimation, this);
			Storyboard.SetTargetProperty(moveRightBackwardAnimation, new PropertyPath("(Node.ZPosition)"));


			// Move front
			this._moveToFrontStoryboard = new Storyboard();
			var moveToFrontAnimation = new DoubleAnimation();
			moveToFrontAnimation.To = 0d;
			moveToFrontAnimation.Duration = TimeSpan.FromMilliseconds(CoverMoveDuration);
			this._moveToFrontStoryboard.Children.Add(moveToFrontAnimation);
			Storyboard.SetTarget(moveToFrontAnimation, this);
			Storyboard.SetTargetProperty(moveToFrontAnimation, new PropertyPath("(Node.YRotation)"));

			var offsetToFrontAnimation = new DoubleAnimation();
			offsetToFrontAnimation.To = 0;
			offsetToFrontAnimation.Duration = TimeSpan.FromMilliseconds(CoverMoveDuration);
			this._moveToFrontStoryboard.Children.Add(offsetToFrontAnimation);
			Storyboard.SetTarget(offsetToFrontAnimation, this);
			Storyboard.SetTargetProperty(offsetToFrontAnimation, new PropertyPath("(Node.XPosition)"));

			var moveForwardAnimation = new DoubleAnimation();
			moveForwardAnimation.To = 20;
			moveForwardAnimation.Duration = TimeSpan.FromMilliseconds(CoverMoveDuration);
			this._moveToFrontStoryboard.Children.Add(moveForwardAnimation);
			Storyboard.SetTarget(moveForwardAnimation, this);
			Storyboard.SetTargetProperty(moveForwardAnimation, new PropertyPath("(Node.ZPosition)"));


		}

		public void MoveToLeft()
		{
			this._moveToLeftStoryboard.Begin();
			this._moveToRightStoryboard.Pause();
			this._moveToFrontStoryboard.Pause();
		}

		public void MoveToFront()
		{
			this._moveToLeftStoryboard.Pause();
			this._moveToRightStoryboard.Pause();
			this._moveToFrontStoryboard.Begin();
		}

		public void MoveToRight()
		{
			this._moveToLeftStoryboard.Pause();
			this._moveToRightStoryboard.Begin();
			this._moveToFrontStoryboard.Pause();
		}
	}
}
