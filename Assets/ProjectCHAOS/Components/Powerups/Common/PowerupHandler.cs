using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WaterToolkit;

namespace ProjectCHAOS.Powerups
{
	public class PowerupHandler : MonoBehaviour
	{
		[SerializeField]
		private Flyweight<PowerupSpec> _powerups = null;

		public Flyweight<PowerupSpec> powerups => _powerups;

		private List<object> _references = new List<object>();

		private void OnPowerupAdd(PowerupSpec powerup)
		{
			powerup.Initialize(_references);
			powerup.Use();
		}

		private void OnPowerupRemove(PowerupSpec powerup)
		{
			powerup.Revoke();
		}

		public void Initialize(IEnumerable<object> references)
		{
			_references.AddRange(references);
		}

		private void Awake()
		{
			Initialize(new object[] { this });	
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
