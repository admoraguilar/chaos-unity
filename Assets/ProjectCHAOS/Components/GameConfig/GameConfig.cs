using UnityEngine;

namespace ProjectCHAOS.Configurations
{
	public class GameConfig : MonoBehaviour
	{
		public int targetFramerate
		{
			get => _targetFramerate;
			set {
				_targetFramerate = value;
				Application.targetFrameRate = _targetFramerate;
			}
		}

		[SerializeField]
		private int _targetFramerate = 60;

		private void Awake()
		{
			targetFramerate = targetFramerate;
		}
	}
}
