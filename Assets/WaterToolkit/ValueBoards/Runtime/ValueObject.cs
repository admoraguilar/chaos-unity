using System;
using System.Collections.Generic;

using URandom = UnityEngine.Random;

namespace WaterToolkit.ValueBoards
{
    public class ValueObject
    {
        private string _key;
        private Dictionary<string, object> _values;

        public string key => _key;
        public IReadOnlyDictionary<string, object> values => _values;

		public ValueObject()
		{
			Initialize();
		}

        public ValueObject(string key)
        {
			Initialize(key);
        }

        public ValueObject(string key, IDictionary<string, object> values)
        {
			Initialize(key, values);
        }

		protected virtual void Initialize(string key = null, IDictionary<string, object> values = null)
		{
			_key = key != null ? key : URandom.Range(0, int.MaxValue).ToString();
			_values = values != null ? new Dictionary<string, object>(values) : new Dictionary<string, object>();
		}

        public virtual bool IsValid()
        {
            return _key != string.Empty && _values.Count > 0;
        }

        public bool TryGetValue<TValue>(string key, out TValue value)
        {
            object result = default;
            bool doesExist = _values.TryGetValue(key, out result);
            if(doesExist) { value = (TValue)result; }
            else { value = default; }
            return doesExist;
        }

        public TValue GetValue<TValue>(string key)
        {
            return (TValue)_values[key];
        }

        public void SetValue(string key, object value)
        {
            _values[key] = value;
        }

        public void AddValue(string key, object value)
        {
            _values[key] = value;
        }

        public void RemoveValue(string key)
        {
            _values.Remove(key);
        }

        public void ClearValues()
        {
            _values.Clear();
        }

        public override string ToString()
        {
            string result = string.Empty;
            result += $"Key: {_key} | ";
            foreach(KeyValuePair<string, object> value in _values)
            {
                result += $"{value.Key}: {value.Value} | ";
            }
            result += $"{Environment.NewLine}";
            return result;
        }
    }
}