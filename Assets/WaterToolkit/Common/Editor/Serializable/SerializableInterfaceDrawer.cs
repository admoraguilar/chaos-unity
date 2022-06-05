using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UObject = UnityEngine.Object;

namespace WaterToolkit.Editor
{
	[CustomPropertyDrawer(typeof(SerializableInterface), true)]
	public class SerializableInterfaceDrawer : PropertyDrawer
	{
		public class SerializableInterfaceProperties
		{
			public SerializedProperty objectProp = null;
			public Type objectType = null;

			public SerializableInterfaceProperties(PropertyDrawer propertyDrawer, SerializedProperty property)
			{
				objectProp = property.FindPropertyRelative(_objectPropName);

				Type propertyType = GetPropertyType(propertyDrawer);
				if(propertyType.BaseType == typeof(SerializableInterface)) {
					objectType = propertyType.GetGenericArguments()[0];
				} else {
					objectType = propertyType.BaseType.GetGenericArguments()[0];
				}
			}

			private Type GetPropertyType(PropertyDrawer propertyDrawer)
			{
				Type fieldType = propertyDrawer.fieldInfo.FieldType;
				if(fieldType.IsArray) {
					return fieldType.GetElementType();
				} else if(fieldType.IsGenericType &&
						  fieldType.GetGenericTypeDefinition() == typeof(List<>)) {
					return fieldType.GetGenericArguments()[0];
				} else {
					return fieldType;
				}
			}
		}

		private const string _objectPropName = "_object";

		private void DrawProperty(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializableInterfaceProperties properties = new SerializableInterfaceProperties(this, property);

			if(property.hasMultipleDifferentValues) {
				EditorGUI.showMixedValue = true;
			}
			
			using(EditorGUI.PropertyScope ps1 = new EditorGUI.PropertyScope(position, label, property)) {
				using(EditorGUI.ChangeCheckScope ccs1 = new EditorGUI.ChangeCheckScope()) {
					UObject obj = EditorGUI.ObjectField(
						position,
						label.text != string.Empty ? $"{property.displayName}.{properties.objectType.Name}" : label.text,
						properties.objectProp.objectReferenceValue,
						typeof(UObject),
						true
					);

					dynamic desiredObj = null;
					if(obj != null) {
						// Try ScriptableObject types
						if(desiredObj == null) {
							Type objType = obj.GetType();
							bool isCanCastObj = properties.objectType.IsAssignableFrom(objType);
							if(isCanCastObj) { desiredObj = obj; }
						}

						// Try GameObject or Component types
						if(desiredObj == null) {
							GameObject go = obj as GameObject;
							if(go == null) {
								Component comp = obj as Component;
								if(comp != null) { go = comp.gameObject; }
							}

							if(go != null) { desiredObj = go.GetComponent(properties.objectType); }
						}

						if(desiredObj == null) {
							EditorUtility.DisplayDialog("SerializableInterface Error", $"Not a valid {properties.objectType.GetClassName()}", "OK");
						}
					}

					if(ccs1.changed) {
						// Assign to the field
						properties.objectProp.objectReferenceValue = desiredObj;
					}
				}

				if(property.hasMultipleDifferentValues) {
					EditorGUI.showMixedValue = false;
				}
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			DrawProperty(position, property, label);
		}
	}
}
