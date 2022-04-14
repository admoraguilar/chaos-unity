using UnityEngine;
using ProjectCHAOS.Systems;
using ProjectCHAOS.Gameplay.Behave;
using ProjectCHAOS.Gameplay.Levels;

namespace ProjectCHAOS.Gameplay.Characters.AIs
{
	public class BasicAI : MonoBehaviour
	{
		private Movement _movement = new Movement();
		private Positioning _positioning = new Positioning();
		private Targetting _targetting = new Targetting();

		[SerializeField]
		private LevelArea _levelArea = null;

		private Transform _transform = null;

		public Movement movement
		{
			get => _movement;
			private set => _movement = value;
		}

		public Positioning positioning
		{
			get => _positioning;
			private set => _positioning = value;
		}

		public Targetting targetting
		{
			get => _targetting;
			private set	=>_targetting = value;
		}

		public LevelArea levelArea
		{
			get => _levelArea;
			private set => _levelArea = value;
		}

		public new Transform transform => this.GetCachedComponent(ref _transform);

		public void Initialize(LevelArea levelArea)
		{
			this.levelArea = levelArea;

			positioning.Initialize(levelArea);
			movement.Initialize(transform);
			targetting.Initialize(transform);

			Reposition();
		}

		public void Reposition()
		{
			if(!positioning.isFunctional || !targetting.isFunctional ||
			   !movement.isFunctional) { return; }

			targetting.targetPoint = positioning.GetPosition();

			movement.space = Space.World;
			movement.speed = 10f;
			movement.direction = targetting.GetDirectionToTarget();
		}

		private void Start()
		{
			if(levelArea != null) { 
				Initialize(levelArea); 
			}
		}

		private void FixedUpdate()
		{
			if(!targetting.isFunctional || !movement.isFunctional) { return; }

			if(targetting.GetDistanceToTarget() > 0.5f) {
				movement.Update();
			}
		}

#if UNITY_EDITOR

		private void OnDrawGizmos()
		{
			if(Application.isPlaying) {
				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere(targetting.targetPoint, 1f);
				Gizmos.DrawRay(transform.position, movement.direction);
			}
		}

#endif
	}
}