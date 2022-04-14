using UnityEngine;
using ProjectCHAOS.Common;

namespace ProjectCHAOS.Behave
{
	public class TransformOperations : MonoBehaviour
	{
		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public void DetachFromParent()
		{
			transform.SetParent(null, true);
		}
	}
}
