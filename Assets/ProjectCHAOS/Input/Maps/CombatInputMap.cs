using UnityEngine;

namespace ProjectCHAOS
{
	public interface ICombatInputMap
	{
		public bool isFiringDown { get; }
		public bool isFiringUp { get; }
	}

	public class PCCombatInputMap : ICombatInputMap
	{
		public bool isFiringDown => Input.GetKeyDown(KeyCode.Mouse0);
		public bool isFiringUp => Input.GetKeyUp(KeyCode.Mouse0);
	}
}
