using System.Collections.Generic;
using UnityEngine;

namespace WaterToolkit.Procedural
{
	public class PrefabSpawnSelect : MonoBehaviour
	{
		[SerializeField]
		private List<Transform> _prefabs = null;

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		private void Start()
		{
			Transform prefab = _prefabs.Random();
			Transform instance = Instantiate(prefab, transform, false);
		}
	}
}
