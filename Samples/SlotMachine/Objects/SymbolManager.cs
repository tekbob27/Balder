using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Linq;

namespace SlotMachine.Objects
{
	public class SymbolCombination : IComparable
	{
		public SymbolType LeftSymbol { get; set; }
		public SymbolType CenterSymbol { get; set; }
		public SymbolType RightSymbol { get; set; }

		public int Score { get; set; }

		#region IComparable Members

		public int CompareTo(object obj)
		{
			return -((SymbolCombination)obj).Score.CompareTo(this.Score);
		}

		#endregion
	}

	public class SymbolManager 
	{
		public static readonly SymbolManager Instance = new SymbolManager();

		private SymbolCombination[] _combinations = new SymbolCombination[] {
			#region Combinations
			new SymbolCombination() 
			{ 
				LeftSymbol = SymbolType.Apple,
				CenterSymbol = SymbolType.Nothing,
				RightSymbol = SymbolType.Nothing,
				Score = 10
			},

			new SymbolCombination() 
			{ 
				LeftSymbol = SymbolType.Apple,
				CenterSymbol = SymbolType.Apple,
				RightSymbol = SymbolType.Nothing,
				Score = 20
			},

			new SymbolCombination() 
			{ 
				LeftSymbol = SymbolType.Apple,
				CenterSymbol = SymbolType.Apple,
				RightSymbol = SymbolType.Apple,
				Score = 30
			},

			new SymbolCombination() 
			{ 
				LeftSymbol = SymbolType.Banana,
				CenterSymbol = SymbolType.Banana,
				RightSymbol = SymbolType.Banana,
				Score = 50
			},

			new SymbolCombination() 
			{ 
				LeftSymbol = SymbolType.Lemon,
				CenterSymbol = SymbolType.Lemon,
				RightSymbol = SymbolType.Lemon,
				Score = 100
			},

			new SymbolCombination() 
			{ 
				LeftSymbol = SymbolType.Pineapple,
				CenterSymbol = SymbolType.Pineapple,
				RightSymbol = SymbolType.Pineapple,
				Score = 200
			},

			new SymbolCombination() 
			{ 
				LeftSymbol = SymbolType.Bell,
				CenterSymbol = SymbolType.Bell,
				RightSymbol = SymbolType.Bell,
				Score = 500
			},

			new SymbolCombination() 
			{ 
				LeftSymbol = SymbolType.Goldbar,
				CenterSymbol = SymbolType.Goldbar,
				RightSymbol = SymbolType.Goldbar,
				Score = 1000
			}
			#endregion
		};

		private SymbolManager()
		{
		}


		public int CheckScore(Wheels wheels)
		{
			var combinations = from combination in this._combinations
							   orderby combination.Score descending
							   select combination;

			Wheel[] wheelsToCheck = new Wheel[] {
				wheels.LeftWheel,
				wheels.CenterWheel,
				wheels.RightWheel
			};

			SymbolType[] combinationSymbolTypes = new SymbolType[3];

			var sortedWheels = from wheel in wheelsToCheck
							   orderby wheel.Symbol.SymbolType
							   select wheel;

			foreach (var combination in combinations)
			{
				int combinationCount = 0;
				combinationSymbolTypes[0] = combination.LeftSymbol;
				combinationSymbolTypes[1] = combination.CenterSymbol;
				combinationSymbolTypes[2] = combination.RightSymbol;

				int combinationIndex = 0;
				foreach (var wheel in sortedWheels)
				{
					SymbolType combinationType = combinationSymbolTypes[combinationIndex];
					combinationIndex++;
					
					if (combinationType == SymbolType.Nothing ||
						wheel.Symbol.SymbolType == combinationType)
					{
						if (wheel.Symbol.SymbolType == combinationType)
						{
							wheel.GivesScore = true;
						}
						combinationCount++;
					}
				}

				if (combinationCount == 3)
				{
					return combination.Score;
				}
				else
				{
					foreach (Wheel wheel in wheelsToCheck)
					{
						wheel.GivesScore = false;
					}
				}
			}

			return 0;
		}


	}
}
