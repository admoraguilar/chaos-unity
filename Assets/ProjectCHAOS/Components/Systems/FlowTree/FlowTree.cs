using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Systems.FlowTrees
{
	public class FlowTree : MonoBehaviour
	{
		private readonly static string _currentPrepend = "[Current] ";

		public event Action<Node> OnAttemptSetSameNodeAsCurrent = delegate { };

		internal event Action OnDestroyCallback = delegate { };

		public Node root = null;

		public bool isLessThanOrOneNode
		{
			get {
				UpdateNodeHistorySize();
				return _nodeHistory.Count <= 1 && _nodeHistory[0].Count <= 0 &&
					   _nodeStack.Count <= 1;
			}
		}

		public Node current
		{
			get {
				InitializeNodeStackSize();
				return _nodeStack[_nodeStackIndex];
			}
		}
		[SerializeField]
		private List<Node> _nodeStack = new List<Node>();
		private int _nodeStackIndex = 0;

		private List<Stack<Node>> _nodeHistory = new List<Stack<Node>>();
		private bool _isEnableWritingHistory = true;

		public new Transform transform => this.GetCachedComponent(ref _transform);
		private Transform _transform = null;

		[ContextMenu("Next")]
		public void Next()
		{
			if(current == null) { SwapImmediate(root); } 
			else { current?.Next(); }
		}

		public void Backward()
		{
			RunNextFrame(() => BackwardImmediate());
		}

		public void BackwardImmediate()
		{
			UpdateNodeHistorySize();

			if(_nodeHistory.Count <= 0) { return; }
			_isEnableWritingHistory = false;

			Stack<Node> stackHistory = _nodeHistory[_nodeStackIndex];
			if(stackHistory.Count > 0) {
				Node node = stackHistory.Pop();
				InternalSwapImmediate(node);
			} else {
				PopImmediate();
			}

			_isEnableWritingHistory = true;
		}

		public void Set(Node node)
		{
			if(IsAttemptSetSameNodeAsCurrent(node)) { return; }

			RunNextFrame(() => SetImmediate(node));
		}

		public void SetImmediate(Node node)
		{
			if(IsAttemptSetSameNodeAsCurrent(node)) { return; }

			InitializeNodeStackSize();

			while(_nodeStack.Count > 1) {
				PopImmediate();
			}

			InternalSwapImmediate(node);
		}

		public void Push(Node node)
		{
			if(IsAttemptSetSameNodeAsCurrent(node)) { return; }

			RunNextFrame(() => PushImmediate(node));
		}

		public void PushImmediate(Node node)
		{
			if(IsAttemptSetSameNodeAsCurrent(node)) { return; }

			InitializeNodeStackSize();

			if(current == null) {
				InternalSwapImmediate(node);
			} else {
				SuspendNode(current);

				_nodeStack.Add(node);
				_nodeStackIndex++;

				VisitNode(current);
			}
		}

		public void PopUntilRemoved(Node node)
		{
			RunNextFrame(() => PopUntilRemovedImmediate(node));
		}

		public void PopUntilRemovedImmediate(Node node)
		{
			while(_nodeStack.Contains(node)) {
				PopImmediate();
			}
		}

		public void Pop()
		{
			RunNextFrame(PopImmediate);
		}

		public void PopImmediate()
		{
			if(_nodeStack.Count > 1) {
				LeaveNode(current);

				_nodeStack.RemoveAt(_nodeStackIndex);
				_nodeStackIndex--;

				ResumeNode(current);
			} else {
				InternalSwapImmediate(null);
			}
		}

		public void Swap(Node node)
		{
			if(IsAttemptSetSameNodeAsCurrent(node)) { return; }

			RunNextFrame(() => SwapImmediate(node));
		}

		public void SwapImmediate(Node node)
		{
			if(IsAttemptSetSameNodeAsCurrent(node)) { return; }

			InitializeNodeStackSize();
			InternalSwapImmediate(node);
		}

		private void InternalSwapImmediate(Node node)
		{
			UpdateNodeHistorySize();

			Node last = _nodeStack[_nodeStackIndex];
			Node current = _nodeStack[_nodeStackIndex];

			if(SetPropertyUtility.SetClass(ref current, node)) {
				LeaveNode(last);
				_nodeStack[_nodeStackIndex] = current;
				VisitNode(current);

				if(_isEnableWritingHistory && last != null &&
				   last.isIncludeInHistory) {
					_nodeHistory[_nodeStackIndex].Push(last);
				}
			}
		}

		private void InitializeNodeStackSize()
		{
			if(_nodeStack.Count <= _nodeStackIndex) {
				_nodeStackIndex = 0;
				_nodeStack.Add(null);
			}
		}

		private void UpdateNodeHistorySize()
		{
			if(ShouldIncreaseNodeHistorySize()) {
				while(ShouldIncreaseNodeHistorySize()) {
					_nodeHistory.Add(new Stack<Node>());
				}
			} else {
				while(ShouldDecreaseNodeHistorySize()) {
					_nodeHistory.RemoveAt(_nodeHistory.Count - 1);
				}
			}

			bool ShouldIncreaseNodeHistorySize() => _nodeHistory.Count <= _nodeStackIndex;
			bool ShouldDecreaseNodeHistorySize() => _nodeHistory.Count - 1 > _nodeStackIndex;
		}

		private void VisitNode(Node node)
		{
			if(InternalVisitNode("Visit", node)) {
				node.Visit();
			}
		}

		private void LeaveNode(Node node)
		{
			if(InternalLeaveNode("Leave", node)) {
				node.Leave();
			}
		}

		private void ResumeNode(Node node)
		{
			if(InternalVisitNode("Resume", node)) { node.Resume(); }
		}

		private void SuspendNode(Node node)
		{
			if(InternalLeaveNode("Suspend", node)) { node.Suspend(); }
		}

		private bool InternalVisitNode(string operationName, Node node)
		{
			if(node == null) { return false; }

			Debug.Log($"[{typeof(FlowTree)}] {operationName}: {node.name.Replace(_currentPrepend, string.Empty)}");
			node.name = current.name.Insert(0, _currentPrepend);
			return true;
		}

		private bool InternalLeaveNode(string operationName, Node node)
		{
			if(node == null) { return false; }

			Debug.Log($"[{typeof(FlowTree)}] {operationName}: {node.name.Replace(_currentPrepend, string.Empty)}");
			node.name = node.name.Replace(_currentPrepend, string.Empty);
			return true;
		}

		private bool IsAttemptSetSameNodeAsCurrent(Node node)
		{
			if(current == node) {
				OnAttemptSetSameNodeAsCurrent(node);
				return true;
			}

			return false;
		}

		private void RunNextFrame(Action action)
		{
			StartCoroutine(RunNextFrame());

			IEnumerator RunNextFrame()
			{
				yield return new WaitForEndOfFrame();
				action?.Invoke();
			}
		}

		private void Start()
		{
			// Set root
			Next();
		}

		private void OnDestroy()
		{
			SetImmediate(null);

			OnDestroyCallback();
			OnDestroyCallback = null;
		}
	}
}
