using UnityEngine;
using UnityEngine.Events;

namespace ProjectCHAOS.FlowTrees
{
	public class NodeEventListener : MonoBehaviour
	{
		public Node node
		{
			get {
				if(_node == null) {
					_node = GetComponent<Node>();
				}
				return _node;
			}
			private set => _node = value;
		}
		[SerializeField]
		private Node _node = null;

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
			if(node != null) {
				node.OnEnableCallback += _onEnable.Invoke;
				node.OnDisableCallback += _onDisable.Invoke;
				node.OnVisit += _onVisit.Invoke;
				node.OnLeave += _onLeave.Invoke;
				node.OnResume += _onResume.Invoke;
				node.OnSuspend += _onSuspend.Invoke;
			}
		}

		private void OnDisable()
		{
			if(node != null) {
				node.OnEnableCallback -= _onEnable.Invoke;
				node.OnDisableCallback -= _onDisable.Invoke;
				node.OnVisit -= _onVisit.Invoke;
				node.OnLeave -= _onLeave.Invoke;
				node.OnResume -= _onResume.Invoke;
				node.OnSuspend -= _onSuspend.Invoke;
			}
		}

#if UNITY_EDITOR
		private void Reset()
		{
			node = node;
		}
#endif
	}
}
