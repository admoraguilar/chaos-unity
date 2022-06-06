using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Sandbox
{
	public class MaterialTest : MonoBehaviour
	{
		public Material material = null;

		private void Start()
		{
			Color oldColor = material.color;

			MeshRenderer mr = GetComponent<MeshRenderer>();
			mr.material = material;
			material.color = Color.yellow;

			Debug.Log($"Material: {material.name} - {material.color} - {material.GetInstanceID()}");
			Debug.Log($"Mesh Shared Material: {mr.sharedMaterial.name} - {material.color} - {mr.sharedMaterial.GetInstanceID()}");
			Debug.Log($"Mesh Material: {mr.material.name} - {material.color} - {mr.material.GetInstanceID()}");

			mr.sharedMaterial = material;
			Debug.Log("Set shared material");

			Debug.Log($"Material: {material.name} - {material.color} - {material.GetInstanceID()}");
			Debug.Log($"Mesh Shared Material: {mr.sharedMaterial.name} - {material.color} - {mr.sharedMaterial.GetInstanceID()}");
			Debug.Log($"Mesh Material: {mr.material.name} - {material.color} - {mr.material.GetInstanceID()}");
			Debug.Log($"Mesh Material: {mr.material.name} - {material.color} - {mr.material.GetInstanceID()}");

			material.color = oldColor;
		}
	}
}
