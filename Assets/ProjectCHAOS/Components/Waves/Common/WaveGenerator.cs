using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Waves
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Waves/Generator")]
	public class WaveGenerator : ScriptableObject, IReadOnlyList<WaveData>
	{
		[SerializeField]
		private List<WaveOperator> _operators = new List<WaveOperator>();

		[Header("Generated")]
		[SerializeField]
		private List<WaveData> _dataList = new List<WaveData>();

		public WaveData this[int index] => _dataList[index];

		public int Count => _dataList.Count;

		public void Generate()
		{
			_dataList.Clear();

			int index = 0;
			foreach(WaveOperator @operator in _operators) {
				@operator.Operate(index++, _dataList);
			}
		}

		public IEnumerator<WaveData> GetEnumerator() => _dataList.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _dataList.GetEnumerator();

#if UNITY_EDITOR

		[ContextMenu("Generate")]
		private void Editor_Generate()
		{
			Generate();
		}

#endif
	}
}

