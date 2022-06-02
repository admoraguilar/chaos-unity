using UnityEngine;
using WaterToolkit.GameInputs;

namespace ProjectCHAOS.GameInputs
{
	public interface ICombatInputMap : IMap
	{
		public bool isFiringDown { get; }
		public bool isFiringUp { get; }
	}

	public class PCCombatInputMap : ICombatInputMap
	{
		public bool isFiringDown => Input.GetKeyDown(KeyCode.Mouse0);
		public bool isFiringUp => Input.GetKeyUp(KeyCode.Mouse0);

		public void Initialize() { }
		public void Deinitialize() { }
		public void Update() { }
		public void FixedUpdate() { }
		public void LateUpdate() { }
	}
}
