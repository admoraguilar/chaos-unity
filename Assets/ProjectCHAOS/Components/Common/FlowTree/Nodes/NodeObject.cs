using System;
using UnityEngine;

namespace ProjectCHAOS.Common
{
	[CreateAssetMenu(menuName = "Midnight.Unity/Flow Tree/Node Object")]
	public class NodeObject : ScriptableObject
	{
		public static implicit operator Node(NodeObject nodeObject) => nodeObject.node;

		public Action OnEnableCallback = delegate { };
		public Action OnDisableCallback = delegate { };
		public Action OnVisit = delegate { };
		public Action OnLeave = delegate { };
		public Action OnResume = delegate { };
		public Action OnSuspend = delegate { };

		public bool isCurrent => node.isCurrent;

		public Node node { get; private set; }

		internal void Use(Node node)
		{
			this.node = node;

			if(this.node != null) {
				this.node.OnEnableCallback += OnEnableCallback.Invoke;
				this.node.OnDisableCallback += OnDisableCallback.Invoke;
				this.node.OnVisit += OnVisit.Invoke;
				this.node.OnLeave += OnLeave.Invoke;
				this.node.OnResume += OnResume.Invoke;
				this.node.OnSuspend += OnSuspend.Invoke;
			}
		}

		internal void Clear()
		{
			if(node != null) {
				this.node.OnEnableCallback -= OnEnableCallback.Invoke;
				this.node.OnDisableCallback -= OnDisableCallback.Invoke;
				this.node.OnVisit -= OnVisit.Invoke;
				this.node.OnLeave -= OnLeave.Invoke;
				this.node.OnResume -= OnResume.Invoke;
				this.node.OnSuspend -= OnSuspend.Invoke;
			}

			node = null;

			OnVisit = delegate { };
			OnLeave = delegate { };
			OnResume = delegate { };
			OnSuspend = delegate { };
		}

		public void Next()
		{
			node?.Next();
		}

		public void Backward()
		{
			node?.Backward();
		}

		public void BackwardImmediate()
		{
			node?.BackwardImmediate();
		}

		public void Set()
		{
			node?.Set();
		}

		public void SetImmediate()
		{
			node?.SetImmediate();
		}

		public void Push()
		{
			node?.Push();
		}

		public void PushImmediate()
		{
			node?.PushImmediate();
		}

		public void PopUntilRemoved()
		{
			node?.PopUntilRemoved();
		}

		public void PopUntilRemovedImmediate()
		{
			node?.PopUntilRemovedImmediate();
		}

		public void Swap()
		{
			node?.Swap();
		}

		public void SwapImmediate()
		{
			node?.SwapImmediate();
		}
	}
}
