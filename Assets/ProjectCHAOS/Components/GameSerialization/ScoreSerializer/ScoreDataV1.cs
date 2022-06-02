using System;
using System.Collections.Generic;
using WaterToolkit.DataSerialization;

namespace ProjectCHAOS.GameSerialization
{
	[Serializable]
	public class ScoreDataV1 : IObjectVersion
	{
		[Serializable]
		public class Score
		{
			public int id = 0;
			public int best = 0;
		}

		public List<Score> scores = new List<Score>();

		public int objectVersion => 0;

		public IObjectVersion ToPrev() => null;

		public IObjectVersion ToNext() => null;
	}
}
