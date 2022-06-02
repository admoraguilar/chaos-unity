using UnityEngine;
using UnityEngine.Events;

namespace WaterToolkit.Behave
{
	public class HealthEvents : MonoBehaviour
	{
		[SerializeField]
		private UnityEvent<float> _onHealthChanged = null;

		[SerializeField]
		private UnityEvent _onHealthEmpty = null;

		private IHealth _health = null;

		public IHealth health => this.GetCachedComponent(ref _health);

		private void OnHealthChanged(float value)
		{
			_onHealthChanged.Invoke(value);
		}

		private void OnHealthEmpty()
		{
			_onHealthEmpty.Invoke();
		}

		private void OnEnable()
		{
			health.health.OnHealthChanged += OnHealthChanged;
			health.health.OnHealthEmpty += OnHealthEmpty;
		}

		private void OnDisable()
		{
			health.health.OnHealthChanged -= OnHealthChanged;
			health.health.OnHealthEmpty -= OnHealthEmpty;
		}
	}
}

