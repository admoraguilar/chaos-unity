using UnityEngine;
using ProjectCHAOS.Common;
using System;

namespace ProjectCHAOS.Sandbox
{
	public class OnEnemyCollisionResponse : MonoBehaviour
	{
		public event Action OnEnemyCollision = delegate { };

		private void OnCollisionEnter(Collision collision)
		{
			GameObject go = collision.gameObject;
			if(go.CompareTag("Enemy")) {
				OnEnemyCollision();
			}
		}
	}
}
