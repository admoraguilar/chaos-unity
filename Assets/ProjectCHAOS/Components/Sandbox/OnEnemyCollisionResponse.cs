using UnityEngine;
using ProjectCHAOS.Common;

namespace ProjectCHAOS.Sandbox
{
	public class OnEnemyCollisionResponse : MonoBehaviour
	{
		[SerializeField]
		private Node _gameNode = null;

		private void OnCollisionEnter(Collision collision)
		{
			GameObject go = collision.gameObject;
			if(go.CompareTag("Enemy")) {
				_gameNode.Next();
			}
		}
	}
}
