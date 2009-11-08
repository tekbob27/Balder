namespace Balder.Core.Execution
{
	public interface IActor
	{
		void Initialize();
		void LoadContent();
		void Loaded();
		void BeforeUpdate();
		void Update();
		void AfterUpdate();
	}
}