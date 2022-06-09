using System;

namespace WaterToolkit.Behave
{
	[Serializable]
	public class FloatMultiplierModifier : StatModifier<float>
	{
		public float multiplier = 1f;

		public override float Modify(float baseValue, float inModifiedValue)
		{
			return inModifiedValue + (baseValue * multiplier);
		}
	}
}
