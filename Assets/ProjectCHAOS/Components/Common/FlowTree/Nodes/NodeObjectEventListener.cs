using UnityEngine;
using UnityEngine.Events;

namespace ProjectCHAOS.Common
{
	public class NodeObjectEventListener : MonoBehaviour
	{
		[SerializeField]
		private NodeObject _nodeObject = null;

		[SerializeField]
		private UnityEvent _onEnable = null;

		[SerializeField]
		private UnityEvent _onDisable = null;

		[SerializeField]
		private UnityEvent _onVisit = null;

		[SerializeField]
		private UnityEvent _onLeave = null;

		[SerializeField]
		private UnityEvent _onResume = null;

		[SerializeField]
		private UnityEvent _onSuspend = null;

		private void OnEnable()
		{
			if(_nodeObject != null) {
				_nodeObject.OnEnableCallback += _onEnable.Invoke;
				_nodeObject.OnDisableCallback += _onDisable.Invoke;
				_nodeObject.OnVisit += _onVisit.Invoke;
				_nodeObject.OnLeave += _onLeave.Invoke;
				_nodeObject.OnResume += _onResume.Invoke;
				_nodeObject.OnSuspend += _onSuspend.Invoke;
			}
		}

		private void OnDisable()
		{
			if(_nodeObject != null) {
				_nodeObject.OnEnableCallback -= _onEnable.Invoke;
				_nodeObject.OnDisableCallback -= _onDisable.Invoke;
				_nodeObject.OnVisit -= _onVisit.Invoke;
				_nodeObject.OnLeave -= _onLeave.Invoke;
				_nodeObject.OnResume -= _onResume.Invoke;
				_nodeObject.OnSuspend -= _onSuspend.Invoke;
			}
		}
	}
}
