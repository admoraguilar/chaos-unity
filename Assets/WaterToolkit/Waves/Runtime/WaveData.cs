using System;
using UnityEngine;

using UObject = UnityEngine.Object;

namespace WaterToolkit.Waves
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
			this(0, data.type, data.difficultyFactor, data.prefab) { }

		public WaveData(int groupIndex, WaveData data) :
			this(groupIndex, data.type, data.difficultyFactor, data.prefab) { }

		public WaveData(string waveType, float difficultyFactor, GameObject prefab) :
			this(0, waveType, difficultyFactor, prefab) { } 

		public WaveData(int groupIndex, string type, float difficultyFactor, GameObject prefab)
		{
			this.type = type;
			this.difficultyFactor = difficultyFactor;
			this.prefab = prefab;
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

