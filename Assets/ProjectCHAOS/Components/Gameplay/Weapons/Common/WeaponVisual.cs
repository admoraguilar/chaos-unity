using System;
using UnityEngine;


namespace ProjectCHAOS.Gameplay.Weapons
{
	public abstract class WeaponVisual : MonoBehaviour
	{
		public event Action OnFire = delegate { };

		public abstract void StartFiring();
		public abstract void StopFiring();

		protected void InvokeOnFire() => OnFire();
	}
}
