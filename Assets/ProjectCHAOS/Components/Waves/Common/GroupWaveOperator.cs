using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Waves
{
	[CreateAssetMenu(menuName = "ProjectCHAOS/Waves/Operators/Group")]
	public class GroupWaveOperator : WaveOperator
	{
		[SerializeField]
		private int _groupCount = 2;

		public override void Operate(int index, List<WaveData> dataList)
		{
			if(dataList.Count <= 0) { return; }

			List<WaveData> baseData = dataList.ToList();
			for(int i = 0; i < _groupCount; i++) {
				dataList.AddRange(CloneWaves(index, baseData));
				index++;
			}
		}
	}
}

