using System;
using System.Collections.Generic;
using UnityEngine;

using URandom = UnityEngine.Random;

namespace WaterToolkit.Drops
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Drops/Drop Object")]
	public class DropObject : ScriptableObject
	{
		[Range(0f, 100f)]
		public float chance = 0;

		[SerializeField]
		private List<DropBehaviour> _drops = new List<DropBehaviour>();

		public IReadOnlyList<DropBehaviour> drops => _drops;

		public bool CanDrop()
		{
			float toss = Mathf.Lerp(0f, 100f, URandom.value);
			return chance > toss;
		}

		public GameObject Get()
		{
			float total = 0f;
			foreach(DropBehaviour drop in _drops) {
				total += drop.weight;
			}

			float toss = Mathf.InverseLerp(0f, total, URandom.value);
			float iterateTotal = 0f;
			foreach(DropBehaviour drop in _drops) {
				iterateTotal += drop.weight;
				if(toss <= iterateTotal) { return drop.prefab; }
			}

			return null;
		}
	}
}
