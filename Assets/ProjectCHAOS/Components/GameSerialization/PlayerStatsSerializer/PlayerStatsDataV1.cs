using System;
using WaterToolkit.DataSerialization;

namespace WaterToolkit.GameSerialization
{
	[Serializable]
	public class PlayerStatsDataV1 : IObjectVersion
	{
		public float speed = 0f;
		public float fireRate = 0f;

		public int objectVersion => 0;

		public IObjectVersion ToPrev() => null;

		public IObjectVersion ToNext() => null;
	}
}
