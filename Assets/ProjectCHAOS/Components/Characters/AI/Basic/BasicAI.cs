using UnityEngine;
using ProjectCHAOS.Weapons;
using ProjectCHAOS.Utilities;
using ProjectCHAOS.Blackboards;

namespace ProjectCHAOS.Characters.AIs
{
	public class BasicAI : MonoBehaviour
	{
		private CharacterMechanic _characterMechanic = null;

		[SerializeField]
		private Movement _movement = null;

		[SerializeField]
		private Targetting _targetting = null;

		private Transform _transform = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);

		private void Awake()
		{
			SceneBlackboard sceneBlackboard = Blackboard.Get<SceneBlackboard>();
			_characterMechanic = sceneBlackboard.Get<CharacterMechanic>();

			_movement = new Movement();
			_movement.Initialize(transform);

			_targetting = new Targetting();
			_targetting.Initialize(transform);
			_targetting.target = _characterMechanic.transform;
		}

		private void Start()
		{
			_movement.direction = _targetting.GetDirectionToTarget();
		}

		private void FixedUpdate()
		{
			_movement.FixedUpdate();
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