using System.Collections.Generic;
using UnityEngine;

namespace WaterToolkit
{
	public abstract class FlyweightSpecObject<T> : ScriptableObject, IFlyweightObject<T>
		where T : FlyweightSpecObject<T>
	{
		private T _source = null;
		private ReferenceBag _references = new ReferenceBag();
		private bool _isInitialized = false;

		public T source => _source;

		protected ReferenceBag references => _references;

		public void Initialize(IEnumerable<object> references)
		{
			if(_isInitialized) { return; }
			_isInitialized = true;

			this.references.AddRange(references);
			OnInitialize();
		}

		protected virtual void OnInitialize() { }

		public abstract T Clone();

		protected T FinishClone(T instance)
		{
			instance._source = (T)this;
			instance._references = _references.Clone();
			return instance;
		}
	}
}
