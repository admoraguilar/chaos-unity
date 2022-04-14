using UnityEngine;
using Lean.Touch;

namespace ProjectCHAOS.Systems.Inputs
{
	public class LeanTouchInput : MonoBehaviour
	{
		[SerializeField]
		private LeanSwipeBase _swipe = null;

		[SerializeField]
		private LeanFingerTap _tap = null;

		public LeanSwipeBase swipe => _swipe;
		public LeanFingerTap tap => _tap;
	}
}
