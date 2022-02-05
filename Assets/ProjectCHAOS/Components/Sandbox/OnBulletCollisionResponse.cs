using UnityEngine;
using ProjectCHAOS.Scores;
using ProjectCHAOS.Weapons;

namespace ProjectCHAOS.Sandbox
{
	public class OnBulletCollisionResponse : MonoBehaviour
	{
		private Score _score = null;

		private void Awake()
		{
			_score = Scorer.Instance.GetScore(0);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.TryGetComponent(out Bullet bullet)) {
				Destroy(gameObject);
				Destroy(bullet.gameObject);

				_score.current += Random.Range(1, 5);
			}
		}
	}
}
