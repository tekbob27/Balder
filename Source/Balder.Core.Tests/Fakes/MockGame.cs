namespace Balder.Core.Tests.Fakes
{
	public class MockGame : Game
	{
		public bool IsInitializeCalled { get; private set; }
		public bool IsLoadContentCalled { get; private set; }
		public bool IsLoadedCalled { get; private set; }
		public bool IsBeforeDrawCalled { get; private set; }
		public bool IsAfterDrawCalled { get; private set; }
		public bool IsDrawCalled { get; private set; }
		public bool IsUpdateCalled { get; private set; }

		public override void Initialize()
		{
			IsInitializeCalled = true;
		}

		public override void LoadContent()
		{
			IsLoadContentCalled = true;
		}

		public override void Loaded()
		{
			IsLoadedCalled = true;
		}

		public override void BeforeDraw()
		{
			IsBeforeDrawCalled = true;
		}

		public override void AfterDraw()
		{
			IsAfterDrawCalled = true;
		}

		public override void Draw()
		{
			IsDrawCalled = true;
		}

		public override void Update()
		{
			IsUpdateCalled = true;
		}

	}
}
