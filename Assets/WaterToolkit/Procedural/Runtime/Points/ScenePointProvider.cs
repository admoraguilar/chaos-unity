using UnityEngine;

namespace WaterToolkit.Procedural
{
	public class ScenePointProvider : MonoBehaviour, IPointProvider
	{
		public Transform[] points = null;
		public Space space = Space.World;

		public Vector3 GetRandomPoint()
		{
			Transform point = points.Random();
			return space == Space.World ? point.position : point.localPosition;
		}
	}
}
