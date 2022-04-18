using System;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.GameModes
{
	[DefaultExecutionOrder(-1)]
	[Serializable]
	public abstract class GameFlow<TWorld, TSystem>
		where TWorld : GameWorld
		where TSystem : GameSystem<TWorld>
	{
		private TWorld _world = null;
		private TSystem _system = null;

		public TWorld world
		{
			get => _world;
			private set => _world = value;
		}

		public TSystem system
		{
			get => _system;
			private set => _system = value;
		}

		public void Initialize(TWorld world, TSystem system)
		{
			this.world = world;
			this.world.Initialize();

			this.system = system;
			this.system.Initialize(world);

			OnInitialize(this.system, this.world);
		}

		public void OnEnable()
		{
			world.OnEnable();
			system.OnEnable();
			OnDoEnable();
		}

		public void OnDisable()
		{
			world.OnDisable();
			system.OnDisable();
			OnDoDisable();
		}

		public void Start()
		{
			world.Start();
			system.Start();
			OnDoStart();
		}

		protected virtual void OnInitialize(TSystem system, TWorld world) { }

		protected virtual void OnDoEnable() { }

		protected virtual void OnDoDisable() { }

		protected virtual void OnDoStart() { }
	}
}
