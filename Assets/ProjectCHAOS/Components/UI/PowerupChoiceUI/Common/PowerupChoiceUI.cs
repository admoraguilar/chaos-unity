using System;
using System.Collections.Generic;
using UnityEngine;
using WaterToolkit;
using ProjectCHAOS.Powerups;

namespace ProjectCHAOS.UI
{
	public class PowerupChoiceUI : MonoBehaviour
	{
		public Action<PowerupSpec> OnChoiceSelect = delegate { };

		[SerializeField]
		private PowerupChoiceItemUI _itemPrefab = null;

		[SerializeField]
		private RectTransform _container = null;

		[Space]
		[SerializeField]
		private PowerupCollection _powerupCollection = null;

		[SerializeField]
		private int _amount = 3;

		private PowerupHandler _handler = null;
		private List<PowerupChoiceItemUI> _instances = new List<PowerupChoiceItemUI>();

		public void Initialize(PowerupHandler powerupHandler)
		{
			this._handler = powerupHandler;
		}

		public void RandomizeChoices()
		{
			foreach(PowerupChoiceItemUI instance in _instances) {
				Destroy(instance.gameObject);
			}

			_instances.Clear();

			for(int i = 0; i < _amount; i++) {
				PowerupSpec powerup = _powerupCollection.Random();

				PowerupChoiceItemUI instance = Instantiate(_itemPrefab, _container);
				instance.gameObject.SetActive(true);
				_instances.Add(instance);

				instance.name.text = powerup.GetType().Name;
				instance.button.onClick.AddListener(() => {
					_handler.powerups.Add(powerup);
					OnChoiceSelect(powerup);
				});
			}
		}

		private void OnEnable()
		{
			RandomizeChoices();
		}
	}
}
