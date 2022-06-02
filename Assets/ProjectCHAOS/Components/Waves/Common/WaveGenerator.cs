using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Waves
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Waves/Generator")]
	public class WaveGenerator : ScriptableObject
	{
		[SerializeField]
		private List<WaveOperator> _operators = new List<WaveOperator>();

		[Header("Generated")]
		[SerializeField]
		private List<WaveData> _dataList = new List<WaveData>();

		public void Generate()
		{
			_dataList.Clear();

			int index = 0;
			foreach(WaveOperator @operator in _operators) {
				@operator.Operate(index++, _dataList);
			}
		}

#if UNITY_EDITOR

		[ContextMenu("Generate")]
		private void Editor_Generate()
		{
			Generate();
		}

#endif
	}
}

