using UnityEngine;
using UnityEditor;

namespace ProjectCHAOS.Systems.Editor
{
	[CustomPropertyDrawer(typeof(Condition))]
	public class ConditionDrawer : PropertyDrawer
	{
		private const string CONDITIONTYPE_PROPNAME = "type";
		private const string INTCONDITIONTYPE_PROPNAME = "intConditionType";
		private const string INTLEFT_PROPNAME = "intLeft";
		private const string INTRIGHT_PROPNAME = "intRight";
		private const string FLOATCONDITIONTYPE_PROPNAME = "floatConditionType";
		private const string FLOATLEFT_PROPNAME = "floatLeft";
		private const string FLOATRIGHT_PROPNAME = "floatRight";
		private const string BOOLCONDITIONTYPE_PROPNAME = "boolConditionType";
		private const string BOOLLEFT_PROPNAME = "boolLeft";
		private const string STRINGCONDITIONTYPE_PROPNAME = "stringConditionType";
		private const string STRINGLEFT_PROPNAME = "stringLeft";
		private const string STRINGRIGHT_PROPNAME = "stringRight";

		private RectOffset _offsetRect = new RectOffset(5, 5, 2, 2);
		private int _indentLevelWidth = 14;

		private float defaultCtrlHeightVertSpace => EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; 

		private void DrawIntCondition(Rect position, SerializedProperty property, GUIContent label)
		{
			// Int left
			Rect intLeftRect = new Rect(position.x, position.y, position.width * .4f, EditorGUIUtility.singleLineHeight);
			SerializedProperty intLeftProp = property.FindPropertyRelative(INTLEFT_PROPNAME);
			EditorGUI.PropertyField(intLeftRect, intLeftProp, GUIContent.none);

			// Int condition type
			Rect intConditionTypeRect = new Rect(intLeftRect.max.x, position.y, position.width * .2f, EditorGUIUtility.singleLineHeight);
			SerializedProperty intConditionTypeProp = property.FindPropertyRelative(INTCONDITIONTYPE_PROPNAME);
			EditorGUI.PropertyField(intConditionTypeRect, intConditionTypeProp, GUIContent.none);

			// Int right
			Rect intRightRect = new Rect(intConditionTypeRect.max.x, position.y, position.width * .4f, EditorGUIUtility.singleLineHeight);
			SerializedProperty intRightProp = property.FindPropertyRelative(INTRIGHT_PROPNAME);
			EditorGUI.PropertyField(intRightRect, intRightProp, GUIContent.none);
		}

		private void DrawFloatCondition(Rect position, SerializedProperty property, GUIContent label)
		{
			// Float left
			Rect floatLeftRect = new Rect(position.x, position.y, position.width * .4f, EditorGUIUtility.singleLineHeight);
			SerializedProperty floatLeftProp = property.FindPropertyRelative(FLOATLEFT_PROPNAME);
			EditorGUI.PropertyField(floatLeftRect, floatLeftProp, GUIContent.none);

			// Float condition type
			Rect floatConditionTypeRect = new Rect(floatLeftRect.max.x, position.y, position.width * .2f, EditorGUIUtility.singleLineHeight);
			SerializedProperty floatConditionTypeProp = property.FindPropertyRelative(FLOATCONDITIONTYPE_PROPNAME);
			EditorGUI.PropertyField(floatConditionTypeRect, floatConditionTypeProp, GUIContent.none);

			// Float right
			Rect floatRightRect = new Rect(floatConditionTypeRect.max.x, position.y, position.width * .4f, EditorGUIUtility.singleLineHeight);
			SerializedProperty floatRightProp = property.FindPropertyRelative(FLOATRIGHT_PROPNAME);
			EditorGUI.PropertyField(floatRightRect, floatRightProp, GUIContent.none);
		}

		private void DrawBoolCondition(Rect position, SerializedProperty property, GUIContent label)
		{
			// Bool left
			Rect boolLeftRect = new Rect(position.x, position.y, position.width * .5f, EditorGUIUtility.singleLineHeight);
			SerializedProperty boolLeftProp = property.FindPropertyRelative(BOOLLEFT_PROPNAME);
			EditorGUI.PropertyField(boolLeftRect, boolLeftProp, GUIContent.none);

			// Bool condition type
			Rect boolConditionTypeRect = new Rect(boolLeftRect.max.x, position.y, position.width * .5f, EditorGUIUtility.singleLineHeight);
			SerializedProperty boolConditionTypeProp = property.FindPropertyRelative(BOOLCONDITIONTYPE_PROPNAME);
			EditorGUI.PropertyField(boolConditionTypeRect, boolConditionTypeProp, GUIContent.none);
		}

		private void DrawStringCondition(Rect position, SerializedProperty property, GUIContent label)
		{
			// String left
			Rect stringLeftRect = new Rect(position.x, position.y, position.width * .4f, EditorGUIUtility.singleLineHeight);
			SerializedProperty stringLeftProp = property.FindPropertyRelative(STRINGLEFT_PROPNAME);
			EditorGUI.PropertyField(stringLeftRect, stringLeftProp, GUIContent.none);

			// String condition type
			Rect stringConditionTypeRect = new Rect(stringLeftRect.max.x, position.y, position.width * .2f, EditorGUIUtility.singleLineHeight);
			SerializedProperty stringConditionTypeProp = property.FindPropertyRelative(STRINGCONDITIONTYPE_PROPNAME);
			EditorGUI.PropertyField(stringConditionTypeRect, stringConditionTypeProp, GUIContent.none);

			// String right
			Rect stringRightRect = new Rect(stringConditionTypeRect.max.x, position.y, position.width * .4f, EditorGUIUtility.singleLineHeight);
			SerializedProperty stringRightProp = property.FindPropertyRelative(STRINGRIGHT_PROPNAME);
			EditorGUI.PropertyField(stringRightRect, stringRightProp, GUIContent.none);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return (defaultCtrlHeightVertSpace * 2f) + _offsetRect.bottom;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position.min += new Vector2(_indentLevelWidth * (EditorGUI.indentLevel - 1) + 3f, 0f);
			position.max += new Vector2(0f, _offsetRect.bottom);
			EditorGUI.DrawRect(position, Color.grey);

			Rect marginedRect = new Rect(position);
			marginedRect.min += new Vector2(_offsetRect.left, _offsetRect.top);
			marginedRect.max -= new Vector2(_offsetRect.right, _offsetRect.bottom);

			using (EditorGUI.PropertyScope ps1 = new EditorGUI.PropertyScope(marginedRect, label, property)) {
				int indentLevel = 0;
				indentLevel -= EditorGUI.indentLevel;

				using (EditorGUI.IndentLevelScope ils1 = new EditorGUI.IndentLevelScope(indentLevel)) {
					// Condition Type
					Rect conditionTypeRect = new Rect(marginedRect.x, marginedRect.y, marginedRect.width, EditorGUIUtility.singleLineHeight);
					SerializedProperty conditionTypeProp = property.FindPropertyRelative(CONDITIONTYPE_PROPNAME);
					EditorGUI.PropertyField(conditionTypeRect, conditionTypeProp, label);
					marginedRect.y += defaultCtrlHeightVertSpace;

					// Condition
					Condition.Type conditionType = (Condition.Type)conditionTypeProp.enumValueIndex;
					switch (conditionType) {
						case Condition.Type.Int: DrawIntCondition(marginedRect, property, label); break;
						case Condition.Type.Float: DrawFloatCondition(marginedRect, property, label); break;
						case Condition.Type.Bool: DrawBoolCondition(marginedRect, property, label); break;
						case Condition.Type.String: DrawStringCondition(marginedRect, property, label); break;
					}
				}
			}
		}
	}
}
