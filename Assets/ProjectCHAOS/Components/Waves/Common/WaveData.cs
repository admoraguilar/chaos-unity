using System;

namespace ProjectCHAOS.Waves
{
	[Serializable]
	public class WaveData
	{
		public string waveType = string.Empty;
		public float difficultyFactor = 0f;

		private int _groupIndex = 0;

		public WaveData(WaveData data) : 
			this(0, data.waveType, data.difficultyFactor) { }

		public WaveData(int groupIndex, WaveData data) :
			this(groupIndex, data.waveType, data.difficultyFactor) { }

		public WaveData(string waveType, float difficultyFactor) :
			this(0, waveType, difficultyFactor) { } 

		public WaveData(int groupIndex, string waveType, float difficultyFactor)
		{
			this.waveType = waveType;
			this.difficultyFactor = difficultyFactor;
			
			_groupIndex = groupIndex;
		}
	}
}

