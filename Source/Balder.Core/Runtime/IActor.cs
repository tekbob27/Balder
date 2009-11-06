namespace Balder.Core.Runtime
{
	public interface IActor
	{
		void Initialize();
		void LoadContent();
		void Loaded();
		void BeforeDraw();
		void Draw();
		void AfterDraw();
		void BeforeUpdate();
		void Update();
		void AfterUpdate();
	}
}
