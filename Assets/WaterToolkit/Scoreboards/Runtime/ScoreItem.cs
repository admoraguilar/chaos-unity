using System;

namespace WaterToolkit.Scoreboards
{
	[Serializable]
    public class ScoreItem
	{
		public string name = string.Empty;
		public int score = 0;
		public int days = 0;
		public int waves = 0;

		public bool IsValid()
        {
            return string.IsNullOrEmpty(name) && score >= 0;
        }
	}
}