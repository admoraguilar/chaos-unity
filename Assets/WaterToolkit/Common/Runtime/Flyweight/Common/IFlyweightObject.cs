#if UNITY_EDITOR


#endif

namespace WaterToolkit
{
	public interface IFlyweightObject<T>
	{
		T source { get; }

		T Clone();
	}
}
