using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ProjectCHAOS.UI
{
	public class PowerupChoiceItemUI : MonoBehaviour
	{
		[SerializeField]
		private Button _button = null;

		[SerializeField]
		private TMP_Text _name = null;

		public Button button => _button;

		public TMP_Text name => _name;
	}
}
