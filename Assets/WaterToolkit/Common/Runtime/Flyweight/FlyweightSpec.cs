using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR


#endif

namespace WaterToolkit
{
	public abstract class FlyweightSpec<T> : ScriptableObject, IFlyweightObject<T>
		where T : FlyweightSpec<T>
	{
		private T _source = null;
		private ReferenceBag _references = new ReferenceBag();

		public T source => _source;

		protected ReferenceBag references => _references;

		public void Initialize(IEnumerable<object> references)
		{
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
