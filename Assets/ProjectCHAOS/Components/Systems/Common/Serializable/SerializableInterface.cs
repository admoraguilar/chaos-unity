using System;
using UnityEngine;

using UObject = UnityEngine.Object;

namespace ProjectCHAOS.Common
{
	[Serializable]
	public class SerializableInterface<T> : SerializableInterface where T : class
	{
		private T _value = null;

		public SerializableInterface() { }

		public SerializableInterface(T value)
		{
			this.value = value;
		}

		public T value
		{
			get {
				if(_value == null) { _value = _object as T; }
				return _value;
			}
			set {
				UObject typeCheck = value as UObject;
				if(typeCheck == null) {
					Debug.LogError($"[{typeof(SerializableInterface)}] Cannot assign an object that's not of type UnityEngine.Object!");
					return;
				}

				_object = typeCheck;
				_value = _object as T;
			}
		}

		public static implicit operator T(SerializableInterface<T> serializableInterface)
		{
			return serializableInterface.value;
		}
	}

	[Serializable]
	public abstract class SerializableInterface
	{
		[SerializeField]
		protected UObject _object = null;

		public static implicit operator UObject(SerializableInterface serializableInterface)
		{
			return serializableInterface._object;
		}
	}
}