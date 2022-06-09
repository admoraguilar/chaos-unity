using System.Collections.Generic;
using UnityEngine;
using WaterToolkit;

using UObject = UnityEngine.Object;

namespace ProjectCHAOS.Powerups
{
	public class PowerupHandler : MonoBehaviour
	{
		[SerializeField]
		private FlyweightContainer<PowerupSpec> _powerups = null;

		private void OnPowerupAdd(PowerupSpec powerup)
		{
			powerup.Use();
		}

		private void OnPowerupRemove(PowerupSpec powerup)
		{
			powerup.Revoke();
		}

		public void Initialize(IEnumerable<object> references)
		{
			foreach(PowerupSpec powerup in _powerups) {
				powerup.Initialize(references);
			}
		}

		private void Awake()
		{
			Initialize(new object[] { this });	
		}

		private void Start()
		{
			foreach(PowerupSpec powerup in _powerups) {
				powerup.Use();
			}
		}

		private void OnEnable()
		{
			_powerups.OnAdd += OnPowerupAdd;
			_powerups.OnRemove += OnPowerupRemove;
		}

		private void OnDisable()
		{
			_powerups.OnAdd -= OnPowerupAdd;
			_powerups.OnRemove -= OnPowerupRemove;
		}
	}
}
