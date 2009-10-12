using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SlotMachine.Objects
{
	public class ScoreManager
	{
		private static readonly int MaxRounds = 15;
		private static readonly int MaxSwaps = 2;

		public static readonly ScoreManager Instance = new ScoreManager();

		private Canvas _rootCanvas;
		private TextBlock _scoreBoardText;

		public Canvas RootCanvas 
		{
			get { return this._rootCanvas; }
			set
			{
				this._scoreBoardText = (TextBlock)value.FindName("_scoreBoardText");
				this._rootCanvas = value;
			}
		}

		public int Score { get; private set; }
		public int RoundCounter { get; private set; }
		public int SwapCounter { get; private set; }

		private ScoreManager()
		{
			this.Score = 0;
		}

		public void NewGame()
		{
			this.Score = 0;
			this.RoundCounter = 0;
			this.SwapCounter = 0;
		}

		public void NewRound()
		{
			this.RoundCounter++;
			this.SwapCounter = 0;
			this.UpdateScoreText();
		}

		public void NewSwap()
		{
			this.SwapCounter++;
		}

		public bool IsMaxRounds { get { return this.RoundCounter >= MaxRounds; } }
		public bool IsMaxSwaps { get { return this.SwapCounter >= MaxSwaps; } }

		public void AddScore(int score)
		{
			this.Score += score;
			this.UpdateScoreText();
		}

		private void UpdateScoreText()
		{
			if (null != this._scoreBoardText)
			{
				this._scoreBoardText.Text = this.Score.ToString();
			}
		}
	}
}
