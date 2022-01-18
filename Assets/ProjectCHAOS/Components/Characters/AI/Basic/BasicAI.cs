using UnityEngine;
using ProjectCHAOS.Weapons;
using ProjectCHAOS.Utilities;
using ProjectCHAOS.Blackboards;

namespace ProjectCHAOS.Characters.AIs
{
	public class BasicAI : MonoBehaviour
	{
		public Vector3 direction = Vector3.zero;
		public float speed = 10f;

		private CharacterMechanic _characterMechanic = null;

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		private void Awake()
		{
			SceneBlackboard sceneBlackboard = Blackboard.Get<SceneBlackboard>();
			
			_characterMechanic = sceneBlackboard.Get<CharacterMechanic>();
			Debug.Log($"Targetting: {_characterMechanic.name} by {name}");
		}

		private void Start()
		{
			Vector3 dir = _characterMechanic.transform.position - transform.position;
			direction = dir.normalized;
		}

		private void FixedUpdate()
		{
			transform.Translate(direction * speed * Time.deltaTime, Space.Self);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.TryGetComponent(out Bullet bullet)) {
				Destroy(gameObject);
				Destroy(bullet.gameObject);
			}
		}
	}
}