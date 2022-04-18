using UnityEngine;

namespace ProjectCHAOS.Gameplay.GameModes
{
	[DefaultExecutionOrder(-1)]
	public class GameMode<TWorld, TSystem, TFlow> : MonoBehaviour
		where TWorld : GameWorld
		where TSystem : GameSystem<TWorld>
		where TFlow : GameFlow<TWorld, TSystem>
	{
		[SerializeField]
		private TWorld _world = null;

		[SerializeField]
		private TSystem _system = null;

		[SerializeField]
		private TFlow _flow = null;

		public TWorld world => _world;

		public TSystem system => _system;

		public TFlow flow => _flow;

		private void Awake()
		{
			_flow.Initialize(_world, _system);
			OnDoAwake();
		}

		private void OnEnable()
		{
			_flow.OnEnable();
			OnDoEnable();
		}

		private void OnDisable()
		{
			_flow.OnDisable();
			OnDoDisable();
		}

		private void Start()
		{
			_flow.Start();
			OnDoStart();
		}

		protected virtual void OnDoAwake() { }

		protected virtual void OnDoEnable() { }

		protected virtual void OnDoDisable() { }

		protected virtual void OnDoStart() { }
	}
}
