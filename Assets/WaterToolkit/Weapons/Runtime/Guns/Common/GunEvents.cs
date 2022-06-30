using UnityEngine;
using UnityEngine.Events;

namespace WaterToolkit.Weapons
{
	public class GunEvents : MonoBehaviour
	{
		[SerializeField]
		private WeaponVisual _weaponVisual = null;

		[SerializeField]
		private UnityEvent<WeaponFireInfo> _onFire = null;

		private void OnEnable()
		{
			_weaponVisual.OnFire += _onFire.Invoke;
		}

		private void OnDisable()
		{
			_weaponVisual.OnFire -= _onFire.Invoke;
		}
	}
}
