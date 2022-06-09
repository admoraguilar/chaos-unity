using System;

namespace WaterToolkit.Behave
{
	[Serializable]
	public abstract class StatModifier<T>
	{
		public string id = string.Empty;

		public abstract T Modify(T value);
	}
}
