using UnityEngine;
using ProjectCHAOS.Weapons;

namespace ProjectCHAOS.Sandbox
{
	public class OnBulletCollisionResponse : MonoBehaviour
	{
		private void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.TryGetComponent(out Bullet bullet)) {
				Destroy(gameObject);
				Destroy(bullet.gameObject);
			}
		}
	}
}
