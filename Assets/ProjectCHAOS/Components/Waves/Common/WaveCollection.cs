using System.Collections.Generic;
using UnityEngine;
using WaterToolkit.GameDatabases;

namespace ProjectCHAOS.Waves
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Waves/Collection")]
	public class WaveCollection : GameCollection<WaveData>
	{
		[SerializeField]
		private List<WaveOperator> _operators = new List<WaveOperator>();

		public void Generate()
		{
			Clear();

			int index = 0;
			foreach(WaveOperator @operator in _operators) {
				@operator.Operate(index++, _entries);
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

