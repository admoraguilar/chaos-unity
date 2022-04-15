using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Gameplay.Weapons
{
	public class WeaponVisualHolder
	{
		private WeaponVisual _visual = null;

		public WeaponVisual visual
		{
			get => _visual;
			private set => _visual = value;
		}

		public void SetVisual(WeaponVisual visual)
		{

		}
	}
}
