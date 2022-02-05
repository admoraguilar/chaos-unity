using UnityEngine;
using Lean.Touch;
using ProjectCHAOS.Common;

namespace ProjectCHAOS.Menus
{
	public class StartMenuUI : MonoBehaviour
	{
		[SerializeField]
		private Node _startGameNode = null;

		[SerializeField]
		private LeanFingerTap _fingerTap = null;

		private void OnLeanFinger(LeanFinger finger)
		{
			_startGameNode.Next();
		}

		private void OnEnable()
		{
			_fingerTap.OnFinger.AddListener(OnLeanFinger);
		}

		private void OnDisable()
		{
			_fingerTap.OnFinger.RemoveListener(OnLeanFinger);
		}
	}
}
