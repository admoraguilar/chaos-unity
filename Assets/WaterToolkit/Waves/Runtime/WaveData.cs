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
			private set => _groupIndex = value;
		}

		public GameObject instance
		{
			get => _instance;
			private set => _instance = value;
		}

		public WaveData(int groupIndex, WaveData data) :
			this(groupIndex, data.type, data.difficultyFactor, data.prefab) { }

		public WaveData(int groupIndex, string type, float difficultyFactor, GameObject prefab)
		{
			this.type = type;
			this.difficultyFactor = difficultyFactor;
			this.prefab = prefab;
			this.groupIndex = groupIndex;
		}

		internal void CreatePrefabInstance(Transform container)
		{
			instance = UObject.Instantiate(prefab, container);
			instance.name = prefab.name;
		}

		internal void DestroyPrefabInstance()
		{
			UObject.Destroy(instance);
			instance = null;
		}
	}
}

