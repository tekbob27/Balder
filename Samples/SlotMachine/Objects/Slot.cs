using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Balder.Core.Objects;
using Balder.Core.Runtime;
using Balder.Core;
using Balder.Core.Math;

namespace SlotMachine.Objects
{

	public enum SlotState
	{
		Idle,
		Login,
		NewGame,
		NewRound,
		PullArm,
		Rolling,
		Swapping,
		Score,
		EndGame
	}

	/// <summary>
	/// The main SlotMachine statemachine
	/// </summary>
	public class Slot : StateMachine<SlotState>
	{
		#region Private Fields
		private Image _slotMachineImage;

		private Sprite _enterAnimation;
		private Sprite _arm;

		private Sprite _leftButton;
		private Sprite _centerButton;
		private Sprite _rightButton;

		private Balder.Core.Application _application;
		private Canvas _rootCanvas;

		private Wheels _wheels;

		private Storyboard _scoreBoardFadeInStoryboard;
		private Storyboard _scoreBoardFadeOutStoryboard;

		private TextBlock _stateInfoText;

		private bool _gameOver;

		#endregion

		#region Constructor(s)
		public Slot(Canvas rootCanvas, Balder.Core.Application application)
		{
			this._application = application;
			this._rootCanvas = rootCanvas;

			this._wheels = new Wheels(rootCanvas, application);

			this._scoreBoardFadeInStoryboard = (Storyboard)rootCanvas.FindName("_scoreBoardFadeInStoryboard");
			this._scoreBoardFadeOutStoryboard = (Storyboard)rootCanvas.FindName("_scoreBoardFadeOutStoryboard");

			this._stateInfoText = (TextBlock)rootCanvas.FindName("_stateInfoText");

			this._gameOver = false;

			this.LoadAssets();
		}
		#endregion

		#region Private Methods

		private void SetInfoText(string text)
		{
			if (null != this._stateInfoText)
			{
				this._stateInfoText.Text = text;
			}
		}

		private void LoadAssets()
		{
			this._slotMachineImage = (Image)this._rootCanvas.FindName("_slotMachineImage");
			this._slotMachineImage.Visibility = Visibility.Collapsed;

			#region Enter Sprite
			this._enterAnimation = new Sprite();
			this._enterAnimation.AddRange(new ImageRange
			{
				FrameCount = 80,
				ArchivePath = "Assets/SlotMachine/EnterAnim.zip",
				FileName = "enter_",
				FileType = "png",
				NumberingFormat = "{0:00000}",
				ImageWidth = 256,
				ImageHeight = 256,
			});
			this._application.Viewport.Scene.Nodes.Add(this._enterAnimation);
			#endregion

			#region Arm Sprite
			this._arm = new Sprite();
			this._arm.AddRange(new ImageRange
			{
				FrameCount = 51,
				ArchivePath = "Assets/SlotMachine/Arm.zip",
				FileName = "arm_",
				FileType = "png",
				NumberingFormat = "{0:00000}",
				ImageWidth = 64,
				ImageHeight = 160,
			});
			this._arm.Position = new Vector(199,34,0);
			this._arm.Visibility = Visibility.Collapsed;
			this._application.Viewport.Scene.Nodes.Add(this._arm);
			#endregion

			#region Left Button Sprite
			this._leftButton = new Sprite();
			this._leftButton.AddRange(new ImageRange
			{
				FrameCount = 10,
				ArchivePath = "Assets/SlotMachine/LeftButtonPressed.zip",
				FileName = "button_a_",
				FileType = "png",
				NumberingFormat = "{0:00000}",
				ImageWidth = 54,
				ImageHeight = 32,
			});
			this._leftButton.Position = new Vector(39, 98, 0);
			this._leftButton.Visibility = Visibility.Collapsed;
			this._application.Viewport.Scene.Nodes.Add(this._leftButton);
			#endregion

			#region Center Button Sprite
			this._centerButton = new Sprite();
			this._centerButton.AddRange(new ImageRange
			{
				FrameCount = 10,
				ArchivePath = "Assets/SlotMachine/CenterButtonPressed.zip",
				FileName = "button_b_",
				FileType = "png",
				NumberingFormat = "{0:00000}",
				ImageWidth = 48,
				ImageHeight = 32,
			});
			this._centerButton.Position = new Vector(92, 98, 0);
			this._centerButton.Visibility = Visibility.Collapsed;
			this._application.Viewport.Scene.Nodes.Add(this._centerButton);
			#endregion 

			#region Right Button Sprite
			this._rightButton = new Sprite();
			this._rightButton.AddRange(new ImageRange
			{
				FrameCount = 10,
				ArchivePath = "Assets/SlotMachine/RightButtonPressed.zip",
				FileName = "button_c_",
				FileType = "png",
				NumberingFormat = "{0:00000}",
				ImageWidth = 56,
				ImageHeight = 32,
			});
			this._rightButton.Position = new Vector(140, 98, 0);
			this._rightButton.Visibility = Visibility.Collapsed;
			this._application.Viewport.Scene.Nodes.Add(this._rightButton);
			#endregion
		}
		#endregion

		#region States
		#region Idle
		public void OnIdleRun()
		{
			if (this._application.Keyboard.IsKeyEdge(Key.Space))
			{
				this._gameOver = false;
				this.ChangeState(SlotState.Login);
			}
		}
		#endregion

		#region Login
		public void OnLoginEnter()
		{
			this._enterAnimation.Visibility = Visibility.Visible;
			this._enterAnimation.AnimationFinished += delegate
			{
				if (!this._gameOver)
				{
					if (null != this._scoreBoardFadeInStoryboard)
					{
						this._scoreBoardFadeInStoryboard.Begin();
					}
					this.ChangeState(SlotState.NewGame);
				}
			};
			this._enterAnimation.Play();
		}

		public void OnLoginLeave()
		{
			this._arm.Visibility = Visibility.Visible;
		}
		#endregion

		#region NewGame
		public void OnNewGameEnter()
		{
			this._enterAnimation.Visibility = Visibility.Collapsed;
			
			this._slotMachineImage.Visibility = Visibility.Visible;
			this._leftButton.Visibility = Visibility.Visible;
			this.ChangeState(SlotState.NewRound);
		}

		public void OnNewGameRun()
		{
		}
		#endregion

		#region PullArm
		public void OnPullArmEnter()
		{
			if (this._wheels.LeftWheel.IsHeld &&
				this._wheels.CenterWheel.IsHeld &&
				this._wheels.RightWheel.IsHeld)
			{
				this.ChangeState(SlotState.Swapping);
				return;
			}

			this._arm.AnimationFinished += delegate
			{
				this.ChangeState(SlotState.Rolling);
			};
			this._arm.Play();
			
		}
		#endregion

		#region Rolling

		public void OnRollingEnter()
		{
			this._wheels.Roll();
		}

		public void OnRollingRun()
		{
			if (this._wheels.CurrentState != WheelState.Rolling)
			{
				this.ChangeState(SlotState.Swapping);
			}
		}

		public void OnRollingLeave()
		{
		}
		#endregion

		#region Swapping
		public void OnSwappingEnter()
		{
			if (ScoreManager.Instance.IsMaxSwaps)
			{
				this.ChangeState(SlotState.Score);
				return;
			}
			ScoreManager.Instance.NewSwap();

			this._leftButton.Visibility = Visibility.Visible;
			this._centerButton.Visibility = Visibility.Visible;
			this._rightButton.Visibility = Visibility.Visible;

			this.SetInfoText("1,2,3 SWAP - SPACE = ROLL");
		}

		public void OnSwappingRun()
		{
			if (this._application.Keyboard.IsKeyEdge(Key.D1))
			{
				this._leftButton.Play();
				this._wheels.ChangeHoldState(WheelType.Left);
			}
			if (this._application.Keyboard.IsKeyEdge(Key.D2))
			{
				this._centerButton.Play();
				this._wheels.ChangeHoldState(WheelType.Center);
			}
			if (this._application.Keyboard.IsKeyEdge(Key.D3))
			{
				this._rightButton.Play();
				this._wheels.ChangeHoldState(WheelType.Right);
			}
			if (this._application.Keyboard.IsKeyEdge(Key.Space))
			{
				this.ChangeState(SlotState.PullArm);
			}
		}

		public void OnSwappingLeave()
		{
			this._leftButton.Visibility = Visibility.Collapsed;
			this._centerButton.Visibility = Visibility.Collapsed;
			this._rightButton.Visibility = Visibility.Collapsed;

			this.SetInfoText(string.Empty);
		}
		#endregion

		#region NewRound
		public void OnNewRoundEnter()
		{
			ScoreManager.Instance.NewRound();
			this._wheels.NewRound();

			if (ScoreManager.Instance.IsMaxRounds)
			{
				this.ChangeState(SlotState.EndGame);
			}

			this.SetInfoText("Press SPACE to roll");
		}

		public void OnNewRoundRun()
		{
			if (this._application.Keyboard.IsKeyEdge(Key.Space))
			{
				this.ChangeState(SlotState.PullArm);
			}
		}

		public void OnNewRoundLeave()
		{
			this.SetInfoText("");
		}

		#endregion

		#region Score

		private static readonly int NumberOfBlinks = 4;
		private static readonly int DelayBetweenBlinks = 10;
		private int _blinkCount;
		private int _blinkDelayCount;

		public void OnScoreEnter()
		{
			int score = SymbolManager.Instance.CheckScore(this._wheels);
			ScoreManager.Instance.AddScore(score);
			this._blinkCount = NumberOfBlinks;
			this._blinkDelayCount = DelayBetweenBlinks;
			this._wheels.LeftWheel.Symbol.IsLit = false;
			this._wheels.CenterWheel.Symbol.IsLit = false;
			this._wheels.RightWheel.Symbol.IsLit = false;

			if (!this._wheels.LeftWheel.GivesScore &&
				!this._wheels.CenterWheel.GivesScore &&
				!this._wheels.RightWheel.GivesScore)
			{
				this.ChangeState(SlotState.NewRound);
			}
		}

		public void OnScoreRun()
		{
			if (this._blinkDelayCount-- <= 0)
			{
				this._blinkDelayCount = DelayBetweenBlinks;
				this._blinkCount--;
				if (this._blinkCount > 0)
				{
					if (this._wheels.LeftWheel.GivesScore)
					{
						this._wheels.LeftWheel.Symbol.IsLit ^= true;
					}
					if (this._wheels.CenterWheel.GivesScore)
					{
						this._wheels.CenterWheel.Symbol.IsLit ^= true;
					}
					if (this._wheels.RightWheel.GivesScore)
					{
						this._wheels.RightWheel.Symbol.IsLit ^= true;
					}
				}
			}

			if (this._blinkCount < 0)
			{
				this.ChangeState(SlotState.NewRound);
			}

		}
		#endregion

		#region EndGame
		public void OnEndGameEnter()
		{
			this._gameOver = true;
			if (null != this._scoreBoardFadeOutStoryboard)
			{
				this._scoreBoardFadeOutStoryboard.Begin();
			}

			this._arm.Visibility = Visibility.Collapsed;
			this._slotMachineImage.Visibility = Visibility.Collapsed;
			this._leftButton.Visibility = Visibility.Collapsed;
			this._rightButton.Visibility = Visibility.Collapsed;
			this._centerButton.Visibility = Visibility.Collapsed;

			this._enterAnimation.Visibility = Visibility.Visible;
			this._enterAnimation.Play(AnimationBehaviour.Once, AnimationDirection.Backwards);
			this._enterAnimation.AnimationFinished += delegate
			{
				if (this._gameOver)
				{
					this.ChangeState(SlotState.Idle);
				}
			};
		}

		#endregion

		#endregion

		public override SlotState DefaultState
		{
			get { return SlotState.Idle; }
		}
	}
}
