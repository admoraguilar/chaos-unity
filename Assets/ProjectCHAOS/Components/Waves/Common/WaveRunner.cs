using System;
using UnityEngine;
using WaterToolkit;

namespace ProjectCHAOS.Waves
{
	public class WaveRunner : MonoBehaviour
	{
		public event Action<WaveData> OnBeginStep = delegate { };
		public event Action<WaveData> OnEndStep = delegate { };

		[SerializeField]
		private WaveCollection _collection = null;

		private WaveData _data = null;
		private int _index = 0;

		private Transform _transform = null;

		public WaveCollection collection
		{
			get => _collection;
			set {
				_collection = value;
				index = 0;
			}
		}

		public WaveData data
		{
			get => _data;
			private set => _data = value;
		}

		public int index
		{
			get => _index;
			set {
				_index = Mathf.Clamp(value, 0, collection.Count);
				data = collection[index];
			}
		}

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public void BeginStep()
		{
			data.CreatePrefabInstance();
			OnBeginStep(data);
		}

		public void EndStep()
		{
			OnEndStep(data);
			index++;
			data.DestroyPrefabInstance();
		}

		private void Awake()
		{
			index = 0;
		}
	}
}
