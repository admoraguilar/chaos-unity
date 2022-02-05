using UnityEngine;
using ProjectCHAOS.Behave;
using ProjectCHAOS.Levels;
using ProjectCHAOS.Weapons;
using ProjectCHAOS.Common;
using ProjectCHAOS.Blackboards;

namespace ProjectCHAOS.Characters.AIs
{
	public class BasicAI : MonoBehaviour
	{
		private LevelArea _levelArea = null;
		private CharacterMechanic _characterMechanic = null;

		[SerializeField]
		private Positioning _positioning = null;

		[SerializeField]
		private Movement _movement = null;

		[SerializeField]
		private Targetting _targetting = null;

		private Transform _transform = null;
		private Rigidbody _rigidbody = null;
		private Collider _collider = null;

		public new Transform transform => this.GetCachedComponent(ref _transform);
		public new Rigidbody rigidbody => this.GetCachedComponent(ref _rigidbody);
		public new Collider collider => this.GetCachedComponent(ref _collider);

		private void Awake()
		{
			SceneBlackboard sceneBlackboard = Blackboard.Get<SceneBlackboard>();

			_levelArea = sceneBlackboard.Get<LevelArea>();
			_characterMechanic = sceneBlackboard.Get<CharacterMechanic>();

			_positioning = new Positioning();
			_positioning.Initialize(collider, _levelArea);

			_movement = new Movement();
			_movement.Initialize(transform);

			_targetting = new Targetting();
			_targetting.Initialize(transform);
		}

		private void Start()
		{
			_targetting.SetTarget(_positioning.GetPosition());
			
			_movement.space = Space.World;
			_movement.direction = _targetting.GetDirectionToTarget();
		}

		private void FixedUpdate()
		{
			if(_targetting.GetDistanceToTarget() > 0.5f) {
				_movement.Update();
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.TryGetComponent(out Bullet bullet)) {
				Destroy(gameObject);
				Destroy(bullet.gameObject);
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(_targetting.targetPoint, 2f);
			Gizmos.DrawRay(transform.position, _movement.direction);
		}
	}
}