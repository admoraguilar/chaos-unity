using System;

namespace ProjectCHAOS.Gameplay.GameModes
{
	[Serializable]
	public abstract class GameWorld
	{
		public void Initialize() => OnInitialize();

		public void OnEnable() => OnDoEnable();

		public void OnDisable() => OnDoDisable();

		protected virtual void OnInitialize() { }
	
		protected virtual void OnDoEnable() { }

		protected virtual void OnDoDisable() { }
	}
}
