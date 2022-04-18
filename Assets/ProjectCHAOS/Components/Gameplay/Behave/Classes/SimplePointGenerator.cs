using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectCHAOS.Systems;

using URandom = UnityEngine.Random;

namespace ProjectCHAOS.Gameplay.Behave
{
	[Serializable]
	public class SimplePointGenerator
	{
		private IList<Bounds> _boundsList = default;

		public void Initialize(IList<Bounds> bounds)
		{
			_boundsList = bounds;
		}

		public Vector3 GetRandomPoint()
		{
			Bounds bounds = _boundsList.Random();

			return new Vector3(
				URandom.Range(bounds.min.x, bounds.max.x),
				URandom.Range(bounds.max.y, bounds.max.y),
				URandom.Range(bounds.max.z, bounds.max.z));
		}
	}
}
