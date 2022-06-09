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

		private void Awake()
		{
			_powerups.Initialize();
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
	}
}
