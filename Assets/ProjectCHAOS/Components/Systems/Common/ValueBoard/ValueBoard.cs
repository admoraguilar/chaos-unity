using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjectCHAOS.Systems
{
    [Serializable]
    public class ValueBoard<T> where T : ValueObject
    {
		public event Action<T> OnAdd = delegate { };
		public event Action<T> OnRemove = delegate { };

        public ValueBoardDuplicateHandling duplicateHandling = ValueBoardDuplicateHandling.Override;

        private List<T> _entries = new List<T>();
		private int _maxEntryCount = int.MaxValue;

		public T this[int index] => _entries[index];

		public int maxEntryCount
		{
			get => _maxEntryCount;
			set => _maxEntryCount = value;
		}

        public bool TryGet(Predicate<T> match)
        {
            T entry = _entries.Find(match);
            return entry.IsValid();
        }

        public List<T> Get(
            Predicate<T> match, int count = -1,
            IComparer<T> sort = null)
        {
            List<T> temp = _entries.FindAll(match).ToList();
            if(count > -1)
            {
                while(temp.Count > count)
                {
                    temp.RemoveAt(temp.Count - 1);
                }
            }
            if(sort != null) { temp.Sort(sort); }
            return temp;
        }

		public void AddRange(IEnumerable<T> values)
		{
			foreach(T value in values) {
				Add(value);
			}
		}

        public void Add(T value)
        {
			if(_entries.Count > _maxEntryCount) 
			{
				return;
			}

            if(duplicateHandling == ValueBoardDuplicateHandling.Override)
            {
                _entries.Remove(value);
            }

            _entries.Add(value);
			OnAdd(value);
        }

		public void RemoveAll(Predicate<T> match)
		{
			List<T> toRemove = _entries.FindAll(match);
			foreach(T value in toRemove) {
				Remove(value);
			}
		}

		public void Remove(T value)
        {
            _entries.Remove(value);
			OnRemove(value);
        }
    }
}

