using System;
using UnityEngine;
using ProjectCHAOS.Systems.FlowTrees;
using ProjectCHAOS.Gameplay.Characters.Players;

namespace ProjectCHAOS.Gameplay.GameModes.Survival
{
	[Serializable]
	public class SurvivalFlow
	{
		[SerializeField]
		private FlowTree _tree = null;

		[Header("Nodes")]
		[SerializeField]
		private Node _base = null;

		[SerializeField]
		private Node _outside = null;

		[SerializeField]
		private Node _outsideDead = null;

		[SerializeField]
		private Node _reload = null;

		private SurvivalWorld _world = null;
		private SurvivalSystem _system = null;

		public FlowTree tree => _tree;

		public Node @base => _base;

		public Node outside => _outside;

		public Node outsideDead => _outsideDead;

		public Node reload => _reload;

		public SurvivalWorld world
		{
			get => _world;
			private set => _world = value;
		}

		public SurvivalSystem system
		{
			get => _system;
			private set => _system = value;
		}

		private void OnBaseVisit()
		{

		}

		private void OnOutsideVisit()
		{
			world.OnOutsideVisit();
			system.OnOutsideVisit();
		}

		private void OnOutsideDeadVisit()
		{
			world.OnOutsideDeadVisit();
			system.OnOutsideDeadVisit();
			outsideDead.Next();
		}

		private void OnReloadVisit()
		{

		}

		private void OnCharacterBasePortalEnter(PlayerCharacter character)
		{
			@base.Next();
		}

		private void OnPlayerCharacterHealthEmpty()
		{
			outside.Next();
		}

		public void Awake(SurvivalWorld world, SurvivalSystem system)
		{
			this.world = world;
			this.system = system;
		}

		public void OnEnable()
		{
			@base.OnVisit += OnBaseVisit;
			outside.OnVisit += OnOutsideVisit;
			outsideDead.OnVisit += OnOutsideDeadVisit;
			reload.OnVisit += OnReloadVisit;

			world.OnCharacterBasePortalEnter += OnCharacterBasePortalEnter;
			world.OnPlayerCharacterHealthEmpty += OnPlayerCharacterHealthEmpty;
		}

		public void OnDisable()
		{
			@base.OnVisit -= OnBaseVisit;
			outside.OnVisit -= OnOutsideVisit;
			outsideDead.OnVisit -= OnOutsideDeadVisit;
			reload.OnVisit -= OnReloadVisit;

			world.OnCharacterBasePortalEnter -= OnCharacterBasePortalEnter;
			world.OnPlayerCharacterHealthEmpty -= OnPlayerCharacterHealthEmpty;
		}
	}
}
