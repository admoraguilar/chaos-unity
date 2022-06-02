
namespace WaterToolkit.Inputs
{
	public interface IMap
	{
		void Initialize();
		void Deinitialize();
		void Update();
		void FixedUpdate();
		void LateUpdate();
	}
}
