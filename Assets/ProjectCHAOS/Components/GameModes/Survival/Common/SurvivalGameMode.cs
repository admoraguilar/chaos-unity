using ProjectCHAOS.Inputs;
using ProjectCHAOS.Scores;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.GameModes
{
	[DefaultExecutionOrder(-1)]
	public class SurvivalGameMode : MonoBehaviour
	{
		[Header("Systems")]
		[SerializeField]
		private LeanTouchInput _leanTouchInput = null;

		[SerializeField]
		private Scorer _scorer = null;

		[Header("UIs")]
		[SerializeField]
		private TouchUIController _touchUiController = null;

		private void Start()
		{
			_touchUiController.Initialize(_leanTouchInput);
		}
	}
}
