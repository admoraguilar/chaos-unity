using System;

namespace WaterToolkit.Behave
{
	[Serializable]
	public class FloatMultiplierModifier : StatModifier<float>
	{
		public float multiplier = 1f;

		public override float Modify(float value) => value * multiplier;
	}
}
