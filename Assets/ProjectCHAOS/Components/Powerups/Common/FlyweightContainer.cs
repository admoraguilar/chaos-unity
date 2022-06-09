using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectCHAOS.Powerups
{
	public interface IFlyweightObject<T>
	{
		int id { get; }

		T Clone();
	}

	[Serializable]
	public class FlyweightContainer<T> : IEnumerable<T> where T : IFlyweightObject<T>
	{
		[SerializeField]
		private List<T> _list = new List<T>();

		public void Initialize()
		{
			List<T> original = _list.ToList();
			_list.Clear();
			AddRange(original);
		}

		public void AddRange(IEnumerable<T> objs)
		{
			foreach(T item in objs) { Add(item); }
		}

		public void Add(T obj)
		{
			T instance = obj.Clone();
			_list.Add(instance);
		}

		public void RemoveRange(IEnumerable<T> objs)
		{
			foreach(T item in objs) { Remove(item); }
		}

		public void Remove(T obj)
		{
			_list.RemoveAll(item => item.id == obj.id);
		}

		public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => ((IList)_list).GetEnumerator();
	}
}
