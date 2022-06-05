using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using WaterToolkit.Behave;

namespace WaterToolkit.Spawners
{
	public class SimpleSpawner : MonoBehaviour, ISpawner
    {
		public event Action<GameObject> OnSpawn = delegate { };
		public event Action<GameObject> OnDespawn = delegate { };
		public event Action<int> OnCycleFinish = delegate { };

		[SerializeField]
		private PrefabSelectionMethod _selectionMethod = PrefabSelectionMethod.Sequential;

		[SerializeField]
		private UpdateMethod _updateMethod = UpdateMethod.Update;

		[SerializeField]
		private Timing _timer = new Timing();

		[SerializeField]
		private Transform _container = null;

		[SerializeField]
		private bool _isSpawnAtStart = false;

		[SerializeField]
		private List<GameObject> _prefabList = new List<GameObject>();

		private List<GameObject> _runtimePrefabList = new List<GameObject>();
		private List<GameObject> _instanceList = new List<GameObject>();
		private int _cycleCount = 0;
		private int _totalSpawnCount = 0;
		private int _totalDespawnCount = 0;

		private Transform _transform = null;

		public PrefabSelectionMethod selectionMethod
		{
			get => _selectionMethod;
			set => _selectionMethod = value;
		}

		public UpdateMethod updateMethod
		{
			get => _updateMethod;
			set => _updateMethod = value;
		}

		public List<GameObject> prefabList => _prefabList;

		public Timing timer => _timer;

		public bool isSpawnAtStart
		{
			get => _isSpawnAtStart;
			set => _isSpawnAtStart = value;
		}

		public int cycleCount
		{
			get => _cycleCount;
			private set => _cycleCount = value;
		}

		public int totalSpawnCount
		{
			get => _totalSpawnCount;
			private set => _totalSpawnCount = value;
		}

		public int totalDespawnCount
		{
			get => _totalDespawnCount;
			private set => _totalDespawnCount = value;
		}

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public void Run() => timer.Run();

		public void Pause() => timer.Pause();

		public void Stop() => timer.Stop();

		public void Resetup(bool shouldStop = false)
		{
			_runtimePrefabList.Clear();
			timer.Reset(shouldStop);
			cycleCount = 0;
			totalSpawnCount = 0;
			totalDespawnCount = 0;
		}

		public virtual GameObject Spawn()
		{
			GameObject prefab = GetPrefab();
			
			GameObject instance = Instantiate(
				prefab, transform.position, 
				transform.rotation, _container);
			instance.name = prefab.name;
			_instanceList.Add(instance);
			totalSpawnCount++;
			OnSpawn(instance);

			LifecycleEvents lifecycleEvents = instance.GetComponent<LifecycleEvents>();
			if(lifecycleEvents == null) {
				lifecycleEvents = instance.AddComponent<LifecycleEvents>();
			}
			lifecycleEvents.OnDestroyResponse += OnDestroyResponse;

			return instance;

			void OnDestroyResponse()
			{
				lifecycleEvents.OnDestroyResponse -= OnDestroyResponse;
				Despawn(instance);
			}
		}

		public virtual void Despawn(GameObject go)
		{
			totalDespawnCount++;
			OnDespawn(go);
			Destroy(go);
		}

		public virtual IReadOnlyCollection<GameObject> DespawnAll()
		{
			_instanceList.RemoveAll(go => go == null);

			List<GameObject> toDestroy = _instanceList.ToList();
			foreach(GameObject go in _instanceList) {
				toDestroy.Add(go);
				Despawn(go);
			}

			return toDestroy;
		}

		protected GameObject GetPrefab()
		{
			if(_runtimePrefabList.Count <= 0) {
				_runtimePrefabList = prefabList.ToList();
				if(selectionMethod == PrefabSelectionMethod.Random) {
					_runtimePrefabList.Shuffle();
				}
			}

			GameObject prefab = _runtimePrefabList[0];
			_runtimePrefabList.RemoveAt(0);

			if(_runtimePrefabList.Count <= 0) {
				OnCycleFinish(++cycleCount);
			}

			return prefab;
		}

		private void OnTimerReachMax()
		{
			Spawn();
		}

		private void Start()
		{
			if(_isSpawnAtStart) { Run(); }
		}

		private void OnEnable()
		{
			timer.OnReachMax += OnTimerReachMax;
		}

		private void OnDisable()
		{
			timer.OnReachMax -= OnTimerReachMax;
		}

		private void FixedUpdate()
		{
			timer.Update();
		}
	}
}
