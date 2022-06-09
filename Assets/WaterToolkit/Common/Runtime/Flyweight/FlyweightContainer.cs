using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace WaterToolkit
{
	[Serializable]
	public class FlyweightContainer<T> : IReadOnlyList<T> where T : IFlyweightObject<T>
	{
		public event Action<T> OnAdd = delegate { };
		public event Action<T> OnRemove = delegate { };

		[SerializeField]
		private List<T> _list = new List<T>();

		public T this[int index]
		{
			get {
				Initialize();
				return _list[index];
			}
		}

		public int Count => _list.Count;

		private bool isPlaying
		{
			get {
				bool result = false;

#if UNITY_EDITOR

				result = EditorApplication.isPlayingOrWillChangePlaymode;

#endif

				return result;
			}
		}

		private bool _isInitialized = false;

		public void Initialize()
		{
			if(_isInitialized) { return; }
			_isInitialized = true;

			if(isPlaying) {
				List<T> original = _list.ToList();
				_list.Clear();
				AddRange(original);
			}
		}

		public T Find(Predicate<T> match)
		{
			Initialize();

			return _list.Find(match);
		}

		public void AddRange(IEnumerable<T> objs)
		{
			foreach(T item in objs) { Add(item); }
		}

		public void Add(T obj)
		{
			Initialize();

			T instance = isPlaying ? obj.Clone() : obj;
			_list.Add(instance);
			OnAdd(instance);
		}

		public void RemoveRange(IEnumerable<T> objs)
		{
			foreach(T item in objs) { Remove(item); }
		}

		public void Remove(T obj)
		{
			Initialize();

			_list.RemoveAll(item => object.Equals(item.source, obj.source));
			OnRemove(obj);
		}

		public IEnumerator<T> GetEnumerator()
		{
			Initialize();

			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			Initialize();

			return ((IList)_list).GetEnumerator();
		}
	}
}
