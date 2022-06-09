using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaterToolkit.Behave
{
	[Serializable]
	public class Stat<T>
	{
		public event Action<T> OnBaseValueChanged = delegate { };
		public event Action OnModifiersChanged = delegate { };

		[SerializeField]
		private T _baseValue = default;

		[SerializeField]
		private List<StatModifier<T>> _modifiers = new List<StatModifier<T>>();

		[Header("Debug")]
		[SerializeField]
		private T _modifiedValue = default;

		private Func<T> _baseValueGetter = null;
		private Func<T, T> _baseValueSetter = null;
		private Func<T, T> _modifiedValueGetter = null;

		public T baseValue
		{
			get => _baseValueGetter != null ? _baseValueGetter() : _baseValue;
			set {
				_baseValue = _baseValueSetter != null ? _baseValueSetter(value) : value;
				OnBaseValueChanged(_baseValue);
			}
		}

		public T modifiedValue
		{
			get {
				_modifiedValue = _baseValue;
				foreach(StatModifier<T> modifier in _modifiers) {
					_modifiedValue = modifier.Modify(_modifiedValue);
				}
				_modifiedValue = _modifiedValueGetter != null ? _modifiedValueGetter(_modifiedValue) : _modifiedValue;
				return _modifiedValue;
			}
		}

		public Stat(T baseValue)
		{
			Initialize(baseValue);
		}

		public Stat(
			T baseValue, Func<T> baseValueGetter = null, 
			Func<T, T> baseValueSetter = null, Func<T, T> modifiedValueGetter = null, 
			IEnumerable<StatModifier<T>> modifiers = null)
		{
			Initialize(
				baseValue, baseValueGetter, 
				baseValueSetter, modifiedValueGetter, 
				modifiers);
		}

		public void Initialize(
			T baseValue, Func<T> baseValueGetter = null,
			Func<T, T> baseValueSetter = null, Func<T, T> modifiedValueGetter = null, 
			IEnumerable<StatModifier<T>> modifiers = null)
		{
			this._baseValueGetter = baseValueGetter;
			this._baseValueSetter = baseValueSetter;
			this._modifiedValueGetter = modifiedValueGetter;

			this.baseValue = baseValue;
			
			if(modifiers != null) {
				AddRangeModifiers(modifiers);
			}
		}

		public void AddRangeModifiers(IEnumerable<StatModifier<T>> modifiers)
		{
			foreach(StatModifier<T> modifier in modifiers) {
				AddModifier(modifier);
			}
		}

		public void AddModifier(StatModifier<T> modifier)
		{
			_modifiers.Add(modifier);
			OnModifiersChanged();
		}

		public void RemoveRangeModifiers(IEnumerable<StatModifier<T>> modifiers)
		{
			foreach(StatModifier<T> modifier in modifiers) {
				RemoveModifier(modifier);
			}
			OnModifiersChanged();
		}

		public void RemoveModifier(StatModifier<T> modifier)
		{
			_modifiers.RemoveAll(item => item.id == modifier.id);
			OnModifiersChanged();
		}

		public void ReplaceModifier(StatModifier<T> modifier)
		{
			RemoveModifier(modifier);
			AddModifier(modifier);
		}

		public void ClearModifiers()
		{
			_modifiers.Clear();
		}

#if UNITY_EDITOR

		public void OnValidate()
		{
			_modifiedValue = modifiedValue;
		}

#endif
	}
}
