using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace WaterToolkit.GameDatabases
{
	public abstract class GameCollection<T> :
		GameCollection, IList<T>
	{
		public event Action<T> OnAdd = delegate { };
		public event Action<T> OnRemove = delegate { };

		public ItemDuplicateHandling duplicateHandling = ItemDuplicateHandling.Override;

		[SerializeField]
		private List<T> _entries = new List<T>();

		[NonSerialized]
		private List<T> _entriesRuntime = new List<T>();
		private int _maxEntryCount = int.MaxValue;

		public T this[int index] => _entriesRuntime[index];

		public int maxEntryCount
		{
			get => _maxEntryCount;
			set => _maxEntryCount = value;
		}

		public override int Count => _entriesRuntime.Count;

		public bool IsReadOnly => false;

		public bool TryGet(Predicate<T> match)
		{
			T entry = _entriesRuntime.Find(match);
			return entry != null;
		}

		public List<T> Get(
			Predicate<T> match, int count = -1,
			Comparison<T> sort = null)
		{
			List<T> temp = _entriesRuntime.FindAll(match).ToList();
			if(count > -1) {
				while(temp.Count > count) {
					temp.RemoveAt(temp.Count - 1);
				}
			}
			if(sort != null) { temp.Sort(sort); }
			return temp;
		}

		public void AddRange(IEnumerable<T> items)
		{
			foreach(T value in items) {
				Add(value);
			}
		}

		public void Add(T item)
		{
			if(_entriesRuntime.Count > _maxEntryCount) {
				return;
			}

			// This doesn't work...
			if(duplicateHandling == ItemDuplicateHandling.Override) {
				_entriesRuntime.Remove(item);
			}

			_entriesRuntime.Add(item);
			OnAdd(item);
		}

		public void Clear()
		{
			foreach(T item in _entriesRuntime) {
				OnRemove(item);
			}
			_entriesRuntime.Clear();
		}

		public bool Contains(T item) => _entriesRuntime.Contains(item);

		public void CopyTo(T[] array, int arrayIndex) => _entriesRuntime.CopyTo(array, arrayIndex);

		public IEnumerator<T> GetEnumerator() => _entriesRuntime.GetEnumerator();

		public int IndexOf(T item) => _entriesRuntime.IndexOf(item);

		public void Insert(int index, T item) => _entriesRuntime.Insert(index, item);

		public void RemoveAll(Predicate<T> match)
		{
			List<T> toRemove = _entriesRuntime.FindAll(match);
			foreach(T item in toRemove) {
				Remove(item);
			}
		}

		public bool Remove(T item)
		{
			bool result = _entriesRuntime.Remove(item);
			OnRemove(item);
			return result;
		}

		public void RemoveAt(int index)
		{
			T item = _entriesRuntime[index];
			_entriesRuntime.Remove(item);
			OnRemove(item);
		}

		protected virtual void OnEnable()
		{
			_entriesRuntime = _entries.ToList();
		}

		T IList<T>.this[int index]
		{
			get => _entriesRuntime[index];
			set => throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator() => ((IList)_entriesRuntime).GetEnumerator();
	}

	public abstract class GameCollection : ScriptableObject
	{
		public abstract int Count { get; }
	}
}

