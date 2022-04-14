using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjectCHAOS.BaseClasses
{
    [Serializable]
    public class ValueBoard<T> where T : ValueObject
    {
        public ValueBoardDuplicateHandling duplicateHandling = ValueBoardDuplicateHandling.Override;

        private List<T> _entries = new List<T>();

        public bool TryGetValue(Predicate<T> match)
        {
            T entry = _entries.Find(match);
            return entry.IsValid();
        }

        public List<T> GetValues(
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

        public void Add(T value)
        {
            if(duplicateHandling == ValueBoardDuplicateHandling.Override)
            {
                _entries.Remove(value);
            }

            _entries.Add(value);
        }

        public void Remove(T value)
        {
            _entries.Remove(value);
        }
    }
}

