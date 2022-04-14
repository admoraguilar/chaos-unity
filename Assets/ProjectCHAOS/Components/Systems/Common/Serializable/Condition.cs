using System;
using UnityEngine;

namespace ProjectCHAOS.Systems
{
	[Serializable]
	public sealed class Condition
	{
		public enum Type
		{
			Int,
			Float,
			Bool,
			String
		};

		public enum IntConditionType
		{
			GreaterThan,
			LessThan,
			Equals,
			NotEquals
		};

		public enum FloatConditionType
		{
			GreaterThan,
			LessThan,
			Approximately
		};

		public enum BoolConditionType
		{
			True,
			False
		};

		public enum StringConditionType
		{
			Equals,
			Contains,
			StartsWith,
			EndsWith
		};

		public Type type = Type.Int;

		public IntConditionType intConditionType = IntConditionType.GreaterThan;
		public int intLeft = 0;
		public int intRight = 0;

		public FloatConditionType floatConditionType = FloatConditionType.GreaterThan;
		public float floatLeft = 0;
		public float floatRight = 0;

		public BoolConditionType boolConditionType = BoolConditionType.True;
		public bool boolLeft = false;

		public StringConditionType stringConditionType = StringConditionType.Equals;
		public string stringLeft = string.Empty;
		public string stringRight = string.Empty;

		public bool result {
			get {
				switch (type) {
					case Type.Int: return EvaluateIntCondition();
					case Type.Float: return EvaluateFloatCondition();
					case Type.Bool: return EvaluateBoolCondition();
					case Type.String: return EvaluateStringCondition();
					default: return false;
				}
			}
		}

		private bool EvaluateIntCondition()
		{
			switch (intConditionType) {
				case IntConditionType.GreaterThan: return intLeft > intRight;
				case IntConditionType.LessThan: return intLeft < intRight;
				case IntConditionType.Equals: return intLeft == intRight;
				case IntConditionType.NotEquals: return intLeft != intRight;
				default: return false;
			}
		}

		private bool EvaluateFloatCondition()
		{
			switch (floatConditionType) {
				case FloatConditionType.GreaterThan: return floatLeft > floatRight;
				case FloatConditionType.LessThan: return floatLeft < floatRight;
				case FloatConditionType.Approximately: return Mathf.Approximately(floatLeft, floatRight);
				default: return false;
			}
		}

		private bool EvaluateBoolCondition()
		{
			switch (boolConditionType) {
				case BoolConditionType.True: return boolLeft == true;
				case BoolConditionType.False: return boolLeft == false;
				default: return false;
			}
		}

		private bool EvaluateStringCondition()
		{
			switch (stringConditionType) {
				case StringConditionType.Equals: return string.Equals(stringLeft, stringRight);
				case StringConditionType.Contains: return stringLeft.Contains(stringRight);
				case StringConditionType.StartsWith: return stringLeft.StartsWith(stringRight, StringComparison.OrdinalIgnoreCase);
				case StringConditionType.EndsWith: return stringLeft.EndsWith(stringRight, StringComparison.OrdinalIgnoreCase);
				default: return false;
			}
		}
	}
}
