using ProjectCHAOS.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectCHAOS.Behave
{
	public class GunEvents : MonoBehaviour
	{
		[SerializeField]
		private MachineGun _machineGun = null;

		[SerializeField]
		private UnityEvent _onFire = null;

		private void OnEnable()
		{
			_machineGun.OnFire += _onFire.Invoke;
		}

		private void OnDisable()
		{
			_machineGun.OnFire -= _onFire.Invoke;
		}
	}
}
