using System;

namespace ProjectCHAOS.Gameplay.GameModes
{
	[Serializable]
	public abstract class GameSystem<TWorld> 
		where TWorld : GameWorld
	{
		private TWorld _world = null;

		public TWorld world
		{
			get => _world;
			private set => _world = value;
		}

		public void Initialize(TWorld world)
		{
			this.world = world;
			OnInitialize(this.world);
		}

		public void OnEnable() => OnDoEnable();

		public void OnDisable() => OnDoDisable();

		public void Start() => OnDoStart();

		protected virtual void OnInitialize(TWorld world) { }

		protected virtual void OnDoEnable() { }

		protected virtual void OnDoDisable() { }

		protected virtual void OnDoStart() { }
	}
}
