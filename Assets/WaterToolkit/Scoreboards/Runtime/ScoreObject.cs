using System;

namespace WaterToolkit.Scoreboards
{
	[Serializable]
    public class ScoreObject
	{
		public string name = string.Empty;
		public int score = 0;
		public int days = 0;
		public int waves = 0;

		public bool IsValid()
        {
            return name != string.Empty && score >= 0;
        }
    }
}