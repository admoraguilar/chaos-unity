using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WaterToolkit;

namespace ProjectCHAOS.Powerups
{
	public class PowerupHandler : MonoBehaviour
	{
		[SerializeField]
		private FlyweightSpec<PowerupSpec> _powerups = null;

		public FlyweightSpec<PowerupSpec> powerups => _powerups;

		private void OnPowerupAdd(PowerupSpec powerup)
		{
			powerup.Use();
		}

		private void OnPowerupRemove(PowerupSpec powerup)
		{
			powerup.Revoke();
		}

		public void AddReferences(IEnumerable<object> references)
		{
			powerups.AddReferences(references);
		}

		private void Awake()
		{
			AddReferences(new object[] { this });	
		}

		private void Start()
		{
			_powerups.Initialize();
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
