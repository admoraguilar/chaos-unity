using System;

namespace ProjectCHAOS.Gameplay.GameModes
{
	[Serializable]
	public abstract class GameWorld
	{
		public void Initialize() => OnInitialize();

		public void OnEnable() => OnDoEnable();

		public void OnDisable() => OnDoDisable();

		public void Start() => OnDoStart();

		protected virtual void OnInitialize() { }
	
		protected virtual void OnDoEnable() { }

		protected virtual void OnDoDisable() { }

		protected virtual void OnDoStart() { }
	}
}
