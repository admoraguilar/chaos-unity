using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace ProjectCHAOS.Common
{
	public abstract class Node : MonoBehaviour
	{
		public virtual string internal_description => string.Empty;

		public event Action OnEnableCallback = delegate { };
		public event Action OnDisableCallback = delegate { };
		public event Action OnVisit = delegate { };
		public event Action OnLeave = delegate { };
		public event Action OnResume = delegate { };
		public event Action OnSuspend = delegate { };

		[SerializeField]
		private NodeObject _nodeObject = null;

		public bool isHandleGameObjectActiveness
		{
			get => _isHandleGameObjectActive;
			set => _isHandleGameObjectActive = value;
		}
		[SerializeField]
		private bool _isHandleGameObjectActive = false;

		public bool isCurrent => tree.current == this;

		internal virtual bool isIncludeInHistory => true;

		public FlowTree tree 
		{
			get => _tree;
			private set { _tree = value; }
		}
		[SerializeField]
		private FlowTree _tree = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);
		private Transform _transform = null;

		internal void Initialize(FlowTree tree)
		{
			Assert.IsNotNull(tree);

			if(this.tree != null) {
				this.tree.OnDestroyCallback -= Deinitialize;
			}

			this.tree = tree;
			this.tree.OnDestroyCallback += Deinitialize;

			_nodeObject?.Use(this);
			SetActive(false);
		}

		internal void Deinitialize()
		{
			tree = null;
			_nodeObject?.Clear();
			SetActive(false);
		}

		internal void Visit()
		{
			SetActive(true);
			OnDoVisit();
			OnVisit();
		}
		protected virtual void OnDoVisit() { }

		internal void Leave()
		{
			SetActive(false);
			OnDoLeave();
			OnLeave();
		}
		protected virtual void OnDoLeave() { }

		internal void Resume()
		{
			OnDoResume();
			OnResume();
		}
		protected virtual void OnDoResume() { }

		internal void Suspend()
		{
			OnDoSuspend();
			OnSuspend();
		}
		protected virtual void OnDoSuspend() { }

		public virtual void Next() { }

		public void Backward()
		{
			tree.Backward();
		}

		public void BackwardImmediate()
		{
			tree.BackwardImmediate();
		}

		public void Set()
		{
			tree.Set(this);
		}

		public void SetImmediate()
		{
			tree.SetImmediate(this);
		}

		public void Push()
		{
			tree.Push(this);
		}

		public void PushImmediate()
		{
			tree.PushImmediate(this);
		}

		public void PopUntilRemoved()
		{
			tree.PopUntilRemoved(this);
		}

		public void PopUntilRemovedImmediate()
		{
			tree.PopUntilRemovedImmediate(this);
		}

		public void Swap()
		{
			tree.Swap(this);
		}

		public void SwapImmediate()
		{
			tree.SwapImmediate(this);
		}

		public void SetActive(bool value)
		{
			if(isHandleGameObjectActiveness) { gameObject.SetActive(value); } 
			else { enabled = value; }
		}

		protected virtual void OnAwake() { }
		protected virtual void OnDoEnable() { }
		protected virtual void OnDoDisable() { }

		private void Awake()
		{
			if(tree != null && !transform.IsChildOf(tree.transform)) {
				Initialize(tree);
			} else {
				tree = GetComponentInParent<FlowTree>();
				if(tree != null) { Initialize(tree); }
			}

			OnAwake();
		}

		private void OnEnable()
		{
			OnDoEnable();
			OnEnableCallback();
		}

		private void OnDisable()
		{
			OnDoDisable();
			OnDisableCallback();
		}

		protected virtual void OnDestroy()
		{
			Deinitialize();
		}
	}
}
