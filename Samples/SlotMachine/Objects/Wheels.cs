using System;
using System.Windows.Controls;
using Balder.Core.Runtime;

namespace SlotMachine.Objects
{
	public enum WheelState
	{
		Idle=1,
		Rolling,
		Swapping
	}

	public class Wheels : StateMachine<WheelState>
	{
		public Wheel LeftWheel { get; private set; }
		public Wheel CenterWheel { get; private set; }
		public Wheel RightWheel { get; private set; }

		private Canvas _rootCanvas;
		private Balder.Core.Application _application;

		public Wheels(Canvas rootCanvas, Balder.Core.Application application)
		{
			this._rootCanvas = rootCanvas;
			this._application = application;

			this.LeftWheel = new Wheel(application, WheelType.Left);
			this.CenterWheel = new Wheel(application, WheelType.Center);
			this.RightWheel = new Wheel(application, WheelType.Right);

			this.LeftWheel.RollFinished += new EventHandler(_leftWheel_RollFinished);
			this.CenterWheel.RollFinished += new EventHandler(_centerWheel_RollFinished);
			this.RightWheel.RollFinished += new EventHandler(_rightWheel_RollFinished);
		}


		void _leftWheel_RollFinished(object sender, EventArgs e)
		{
			if (this.CenterWheel.IsHeld && this.RightWheel.IsHeld)
			{
				this.ChangeState(WheelState.Swapping);
			}
		}

		void _centerWheel_RollFinished(object sender, EventArgs e)
		{
			if (this.RightWheel.IsHeld)
			{
				this.ChangeState(WheelState.Swapping);
			}
		}

		void _rightWheel_RollFinished(object sender, EventArgs e)
		{
			this.ChangeState(WheelState.Swapping);
		}

		public void NewRound()
		{
			this.LeftWheel.Reset();
			this.CenterWheel.Reset();
			this.RightWheel.Reset();
		}


		public void Roll()
		{
			this.ChangeState(WheelState.Rolling);
		}

		public void ChangeHoldState(WheelType type)
		{
			Wheel wheel = null;
			switch (type)
			{
				case WheelType.Left:
					{
						wheel = this.LeftWheel;
					} break;
				case WheelType.Center:
					{
						wheel = this.CenterWheel;
					} break;
				case WheelType.Right:
					{
						wheel = this.RightWheel;
					} break;
			}
			if (null != wheel)
			{
				wheel.ChangeHoldState();
			}
		}

		public void OnRollingEnter()
		{
			if (!this.LeftWheel.IsHeld)
			{
				this.LeftWheel.Roll(delegate
				{
					if (!this.CenterWheel.IsHeld)
					{
						this.CenterWheel.Roll(delegate
						{
							this.RightWheel.Roll();
						});
					}
					else
					{
						this.RightWheel.Roll();
					}
				});
			}
			else if (!this.CenterWheel.IsHeld)
			{
				this.CenterWheel.Roll(delegate
				{
					this.RightWheel.Roll();
				});
			}
			else
			{
				this.RightWheel.Roll();
			}
		}

		public void OnSwappingEnter()
		{
			
		}

		public override WheelState DefaultState
		{
			get { return WheelState.Idle; }
		}
	}
}
