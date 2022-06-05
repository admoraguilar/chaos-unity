using System;
using UnityEngine;

namespace WaterToolkit.Waves
{
	public class WaveRunner : MonoBehaviour
	{
		public event Action OnStart = delegate { };
		public event Action OnComplete = delegate { };
		public event Action<WaveData> OnBeginStep = delegate { };
		public event Action<WaveData> OnEndStep = delegate { };

		[SerializeField]
		private WaveCollection _collection = null;

		private WaveData _data = null;
		private int _index = 0;
		private bool _hasBegunStep = false;

		private Transform _transform = null;

		public WaveCollection collection
		{
			get => _collection;
			set {
				if(_hasBegunStep) { EndStep(); }

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

		public WaveData BeginStep()
		{
			_hasBegunStep = true;
			if(index <= 0) { OnStart(); }

			data.CreatePrefabInstance(transform);
			OnBeginStep(data);

			return data;
		}

		public WaveData EndStep()
		{
			_hasBegunStep = false;
			OnEndStep(data);
			data.DestroyPrefabInstance();

			index++;
			if(index >= collection.Count) { OnComplete(); }

			return data;
		}

		private void Awake()
		{
			index = 0;
		}
	}
}
