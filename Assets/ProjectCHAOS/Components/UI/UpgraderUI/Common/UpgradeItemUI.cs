using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WaterToolkit.UI
{
	public class UpgradeItemUI : MonoBehaviour
	{
		[SerializeField]
		private Sprite _onSelectSprite = null;

		[SerializeField]
		private Sprite _onNotSelectedSprite = null;

		[Space]
		[SerializeField]
		private Image _background = null;

		[SerializeField]
		private TMP_Text _text = null;

		public Image background => _background;

		public TMP_Text text => _text;

		public void Select(bool value)
		{
			if(value) {
				background.sprite = _onSelectSprite;
				text.color = SetBrightness(text.color, 1f);
			} else {
				background.sprite = _onNotSelectedSprite;
				text.color = SetBrightness(text.color, .8f);
			}
		}

		private Color SetBrightness(Color color, float value)
		{
			float h = 0f;
			float s = 0f;
			float v = 0f;

			Color.RGBToHSV(color, out h, out s, out v);
			v = value;

			return Color.HSVToRGB(h, s, v);
		}
	}
}
