using UnityEngine;
using WaterToolkit;

namespace ProjectCHAOS.Powerups
{
	public class PowerupHandler : MonoBehaviour
	{
		[SerializeField]
		private FlyweightContainer<PowerupSpec> _powerups = null;

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		private void OnPowerupAdd(PowerupSpec powerup)
		{
			powerup.Use();
		}

		private void OnPowerupRemove(PowerupSpec powerup)
		{

		}

		private void Awake()
		{
			foreach(PowerupSpec powerup in _powerups) {
				powerup.Initialize(new object[] { this, transform });
			}
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
