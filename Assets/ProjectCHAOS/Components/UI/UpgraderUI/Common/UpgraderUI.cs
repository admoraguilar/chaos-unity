using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectCHAOS.Upgrades;

namespace ProjectCHAOS.UI
{
	public class UpgraderUI : MonoBehaviour
	{
		public event Action OnShow = delegate { };
		public event Action OnHide = delegate { };

		[SerializeField]
		private UpgradeItemUI _itemUiPrefab = null;

		[SerializeField]
		private RectTransform _itemParent = null;

		[SerializeField]
		private RectTransform _outerPanel = null;

		[Space]
		[SerializeField]
		private Button _showButton = null;

		[SerializeField]
		private Button _upgradeButton = null;

		[SerializeField]
		private Button _rewardedPowerupButton = null;

		private List<UpgradeItemUI> _itemInstances = new List<UpgradeItemUI>();
		private bool _isShown = false;

		private Upgrader _upgrader = null;

		private RectTransform _rectTransform = null;

		public Upgrader upgrader
		{
			get => _upgrader;
			private set => _upgrader = value;
		}

		public RectTransform rectTransform => this.GetCachedComponent(ref _rectTransform);

		public void Initialize(Upgrader upgrader)
		{
			this.upgrader = upgrader;

			foreach(UpgradeItemUI item in _itemInstances) {
				Destroy(item.gameObject);
			}

			foreach(UpgradeObject upgraderObj in upgrader.objects) {
				UpgradeItemUI itemUi = Instantiate(_itemUiPrefab, _itemParent);
				_itemInstances.Add(itemUi);
				itemUi.gameObject.SetActive(true);

				itemUi.text.text = upgraderObj.name;

				upgraderObj.OnUpgrade += OnUpgraderObjUpgrade;
				OnUpgraderObjUpgrade(upgraderObj, null, upgraderObj.index);

				void OnUpgraderObjUpgrade(UpgradeObject obj, Transform upgradable, int index)
				{
					if(index == obj.maxIndex) {
						itemUi.text.text = $"{obj.name.ToUpper()} - MAX";
					} else {
						itemUi.text.text = $"{obj.name.ToUpper()} - X{++index}";
					}
				}
			}

			upgrader.OnObjectIndexChanged += OnObjectIndexChanged;
			OnObjectIndexChanged(upgrader.objectIndex);
		}

		public void Show()
		{
			_isShown = true;

			Vector2 pivot = _outerPanel.pivot;
			_outerPanel.pivot = new Vector2(pivot.x, 0.5f);

			OnShow();
		}

		public void Hide()
		{
			_isShown = false;

			Vector2 pivot = _outerPanel.pivot;
			_outerPanel.pivot = new Vector2(pivot.x, 0.38f);

			OnHide();
		}

		private void OnObjectIndexChanged(int index)
		{
			int i = 0;
			foreach(UpgradeItemUI item in _itemInstances) {
				if(i == index) { item.Select(true); }
				else { item.Select(false); }

				i++;
			}

			if(index > -1) {
				_upgradeButton.interactable = true;
			} else {
				_upgradeButton.interactable = false;
			}
		}

		private void OnShowButtonPressed()
		{
			if(!_isShown) { Show(); }
			else { Hide(); }
		}

		private void OnUpgradeButtonPressed()
		{
			upgrader.Upgrade();
			Hide();

			upgrader.ResetObjectIndex();
		}

		private void OnRewardedPowerupButtonPressed()
		{
			upgrader.objectIndex++;
		}

		private void OnEnable()
		{
			if(upgrader != null) {
				upgrader.OnObjectIndexChanged += OnObjectIndexChanged;
			}

			_showButton.onClick.AddListener(OnShowButtonPressed);
			_upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
			_rewardedPowerupButton.onClick.AddListener(OnRewardedPowerupButtonPressed);
		}

		private void OnDisable()
		{
			if(upgrader != null) {
				upgrader.OnObjectIndexChanged -= OnObjectIndexChanged;
			}

			_showButton.onClick.RemoveListener(OnShowButtonPressed);
			_upgradeButton.onClick.RemoveListener(OnUpgradeButtonPressed);
			_rewardedPowerupButton.onClick.RemoveListener(OnRewardedPowerupButtonPressed);
		}

		private void Start()
		{
			Hide();

			_upgradeButton.interactable = false;
		}
	}
}
