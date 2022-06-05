using System.Linq;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace ProjectCHAOS.Waves
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Waves/Operators/Manual")]
	public class ManualWaveOperator : WaveOperator
	{
		[SerializeField]
		private AnimationCurve _difficultyCurve = null;

		[SerializeField]
		private WaveData[] _data = null;

		public override void Operate(int index, List<WaveData> dataList)
		{
			dataList.AddRange(CloneWaves(index, _data));
		}

		private void SetDifficultyFactor()
		{
			for(int i = 0; i < _data.Length; i++) {
				WaveData data = _data[i];
				data.difficultyFactor = _difficultyCurve.Evaluate(Mathf.InverseLerp(0, _data.Count() - 1, i));
			}
		}

#if UNITY_EDITOR

		[ContextMenu("Set Difficulty Factor")]
		private void Editor_SetDifficultyFactor()
		{
			SetDifficultyFactor();
			EditorUtility.SetDirty(this);
		}

#endif
	}
}

