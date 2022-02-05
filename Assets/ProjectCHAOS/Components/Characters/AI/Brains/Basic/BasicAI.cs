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

		public new Transform transform => this.GetCachedComponent(ref _transform);

		private void Awake()
		{
			SceneBlackboard sceneBlackboard = Blackboard.Get<SceneBlackboard>();

			_levelArea = sceneBlackboard.Get<LevelArea>();
			_characterMechanic = sceneBlackboard.Get<CharacterMechanic>();

			_positioning = new Positioning();
			_positioning.Initialize(_levelArea);

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

#if UNITY_EDITOR

		private void OnDrawGizmos()
		{
			if(Application.isPlaying) {
				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere(_targetting.targetPoint, 1f);
				Gizmos.DrawRay(transform.position, _movement.direction);
			}
		}

#endif
	}
}