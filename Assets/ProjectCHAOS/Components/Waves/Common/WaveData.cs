using System;
using UnityEngine;

using UObject = UnityEngine.Object;

namespace ProjectCHAOS.Waves
{
	[Serializable]
	public class WaveData
	{
		public string type = string.Empty;
		public float difficultyFactor = 0f;
		public GameObject prefab = null;

		private int _groupIndex = 0;

		[NonSerialized]
		private GameObject _instance = null;

		public int groupIndex
		{
			get => _groupIndex;
			set => _groupIndex = value;
		}

		public GameObject instance
		{
			get => _instance;
			private set => _instance = value;
		}

		public WaveData(WaveData data) : 
			this(0, data.type, data.difficultyFactor) { }

		public WaveData(int groupIndex, WaveData data) :
			this(groupIndex, data.type, data.difficultyFactor) { }

		public WaveData(string waveType, float difficultyFactor) :
			this(0, waveType, difficultyFactor) { } 

		public WaveData(int groupIndex, string type, float difficultyFactor)
		{
			this.type = type;
			this.difficultyFactor = difficultyFactor;
			this.groupIndex = groupIndex;
		}

		public void CreatePrefabInstance()
		{
			instance = UObject.Instantiate(prefab);
		}

		public void DestroyPrefabInstance()
		{
			UObject.Destroy(instance);
			instance = null;
		}
	}
}

