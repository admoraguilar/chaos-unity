using UnityEngine;
using ProjectCHAOS;

namespace ProjectCHAOS.Worlds
{
	public class MaterialOffsetScroller : MonoBehaviour
	{
		[SerializeField]
		private Vector2 _scrollDirection = Vector2.zero;

		[SerializeField]
		private float _scrollSpeed = 10f;

		private Renderer _renderer = null;

		public new Renderer renderer => this.GetCachedComponent(ref _renderer);

		private void FixedUpdate()
		{
			renderer.material.mainTextureOffset += _scrollDirection * _scrollSpeed * Time.deltaTime;
		}
	}
}
