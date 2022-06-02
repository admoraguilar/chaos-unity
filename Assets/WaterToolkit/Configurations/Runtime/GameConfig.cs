using UnityEngine;

namespace WaterToolkit.Configurations
{
	public class GameConfig : MonoBehaviour
	{
		[SerializeField]
		private int _targetFramerate = 60;

		public int targetFramerate
		{
			get => _targetFramerate;
			set {
				_targetFramerate = value;
				Application.targetFrameRate = _targetFramerate;
			}
		}

		public void Initialize()
		{
			targetFramerate = targetFramerate;
		}
	}
}
