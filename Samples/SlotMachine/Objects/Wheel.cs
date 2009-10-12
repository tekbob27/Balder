using System;
using System.Windows;
using Balder.Core;
using Balder.Core.Math;
using Balder.Core.Objects;

namespace SlotMachine.Objects
{
	public enum WheelType
	{
		Left = 1,
		Center,
		Right
	}



	public class Wheel
	{
		private static readonly int RollCount = 4;
		private static readonly int RollcountBeforeNext = 2;

		private Sprite _roll;
		public  Symbol Symbol { get; private set; }
		public WheelType WheelType { get; private set; }

		private int _rollCount;

		private Balder.Core.Application _application;

		public event EventHandler RollFinished;

		public Wheel(Balder.Core.Application application, WheelType type)
		{
			this._application = application;

			this._roll = new Sprite();
			this._roll.AddRange(new ImageRange
			{
				FrameCount = 10,
				ArchivePath = this.GetAssetName(type),
				FileName = this.GetFileName(type),
				FileType = "png",
				NumberingFormat = "{0:00000}",
				ImageWidth = 32,
				ImageHeight = 64,
			});
			this._roll.Position = this.GetPosition(type);
			this._roll.Visibility = Visibility.Collapsed;
			application.Viewport.Scene.Nodes.Add(this._roll);

			this.Symbol = new Symbol(type);
			application.Viewport.Scene.Nodes.Add(this.Symbol);
			this.Symbol.Visibility = Visibility.Collapsed;

			this.IsHeld = false;

			this.WheelType = type;
		}


		#region Private Methods
		private Vector GetPosition(WheelType type)
		{
			switch (type)
			{
				case WheelType.Left:
					{
						return new Vector(64, 34, 0);
					};
				case WheelType.Center:
					{
						return new Vector(102, 34, 0);
					};
				case WheelType.Right:
					{
						return new Vector(140, 34, 0);
					};
			}
			return new Vector(0, 0, 0);
		}

		private string GetAssetName(WheelType type)
		{
			switch (type)
			{
				case WheelType.Left:
					{
						return "Assets/SlotMachine/LeftWheelRoll.zip";
					};
				case WheelType.Center:
					{
						return "Assets/SlotMachine/CenterWheelRoll.zip";
					};
				case WheelType.Right:
					{
						return "Assets/SlotMachine/RightWheelRoll.zip";
					};
			}
			return string.Empty;
		}

		private string GetFileName(WheelType type)
		{
			switch (type)
			{
				case WheelType.Left:
					{
						return "wheel_a_";
					};
				case WheelType.Center:
					{
						return "wheel_b_";
					};
				case WheelType.Right:
					{
						return "wheel_c_";
					};
			}
			return string.Empty;
		}
		#endregion

		protected virtual void OnRollFinished()
		{
			if (null != this.RollFinished)
			{
				this.RollFinished(this, new EventArgs());
			}
		}

		public bool IsHeld { get; private set; }

		public bool GivesScore { get; set; }

		public void Reset()
		{
			this.IsHeld = false;
			this.GivesScore = false;
			this.Symbol.IsLit = false;
		}

		public void ChangeHoldState()
		{
			this.Symbol.IsLit ^= true;
			this.IsHeld = this.Symbol.IsLit;
		}


		public void Roll()
		{
			this.Roll(null);
		}

		public void Roll(EventHandler readyForNext)
		{
			if (this.IsHeld)
			{
				return;
			}

			this._rollCount = 0;


			this.Symbol.Randomize();

			if (null != readyForNext)
			{
				this._roll.AnimationLooped += delegate
				{
					if (this._rollCount++ >= RollcountBeforeNext)
					{
						readyForNext(this, new EventArgs());
					}
				};
			}
			this._roll.AnimationFinished += delegate
			{
				this._roll.Visibility = Visibility.Collapsed;
				this.Symbol.Visibility = Visibility.Visible;
				this.OnRollFinished();
			};
			this._roll.Play(AnimationBehaviour.Loop, AnimationDirection.Forward, RollCount);
			this._roll.Visibility = Visibility.Visible;
			this.Symbol.Visibility = Visibility.Collapsed;
		}
	}
}
