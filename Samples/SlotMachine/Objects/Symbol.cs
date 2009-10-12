using System;
using Balder.Core;
using Balder.Core.Math;

namespace SlotMachine.Objects
{
	public enum SymbolType
	{
		Start = 1,
		Apple = Start,
		Banana,
		Bell,
		Goldbar,
		Lemon,
		Pineapple,
		Max = Pineapple,
		Nothing,			
	}


	// TODO: This could probably inherit Sprite and add frames to the sprite and instead just select the correct frame
	public class Symbol : DrawableNode
	{
		
		private ImagePrimitive _image;
		private Random _random;

		private Vector _position;

		public Symbol(WheelType type)
		{
			this._image = new ImagePrimitive();
			this._image.Width = 32;
			this._image.Height = 32;
			this._random = new Random();
			this._image.YPosition = 50;
			this.Randomize();
			this.SetPosition(type);
		}


		private void SetPosition(WheelType type)
		{
			switch (type)
			{
				case WheelType.Left:
					{
						this._image.XPosition = 64;
					} break;
				case WheelType.Center:
					{
						this._image.XPosition = 102;
					} break;
				case WheelType.Right:
					{
						this._image.XPosition = 140;
					} break;
			}
		}

		private void SetImageUrl()
		{
			string fileName = this.SymbolType.ToString().ToLower();

			if (this.IsLit)
			{
				fileName += "_lit";
			}

			this._image.ImageUrl = "Assets/SlotMachine/Symbols/" + fileName + ".png";
		}


		public SymbolType SymbolType { get; /*private*/ set; }
		public bool IsLit { get; set; }


		public void Randomize()
		{
			this.SymbolType = (SymbolType)this._random.Next((int)SymbolType.Max - (int)SymbolType.Start) + (int)SymbolType.Start;
		}

		public override void Render(Viewport3D viewport, Matrix renderMatrix)
		{
			this.SetImageUrl();
			PrimitivesManager.Instance.AddPrimitive(this._image);
		}
	}
}
