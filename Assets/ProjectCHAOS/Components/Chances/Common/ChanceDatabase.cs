using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteDB;
using WaterToolkit;

#if UNITY_EDITOR

using UnityEditor;

#endif

using URandom = UnityEngine.Random;

namespace ProjectCHAOS.Database
{
	public class WaveType
	{
		public int id { get; set; }
		public string type { get; set; }
		public float difficultyFactor { get; set; }
	}

	[CreateAssetMenu(menuName = "ProjectCHAOS/Chances/Chance Database")]
	public class ChanceDatabase : ScriptableObject
	{
		private void GenerateDB()
		{
			string path = PathUtilities.GetPath(Path.Combine(Application.dataPath, "ProjectCHAOS", "Data", "Chaos.db"));
			using(LiteDatabase db = new LiteDatabase(path)) {
				ILiteCollection<WaveType> waveCollection = db.GetCollection<WaveType>("waves");
				waveCollection.DeleteAll();

				List<WaveType> waves = new List<WaveType>();
				for(int i = 0; i < 5; i++) {
					WaveType wave = new WaveType {
						type = "mob",
						difficultyFactor = URandom.Range(0f, 1f)
					};
					waves.Add(wave);
				}
				
				waveCollection.InsertBulk(waves);

				waveCollection.EnsureIndex(x => x.difficultyFactor);

				List<WaveType> results = waveCollection.Query()
					.Where(x => x.difficultyFactor > .5f)
					.OrderBy(x => x.difficultyFactor)
					.ToList();

				foreach(WaveType wave in results) {
					Debug.Log(
						$"[Wave] {Environment.NewLine}" +
						$"Id: {wave.id} {Environment.NewLine}" +
						$"Type: {wave.type} {Environment.NewLine} +" +
						$"Difficulty Factor: {wave.difficultyFactor} {Environment.NewLine}" +
						$"{Environment.NewLine}");
				}
			}
		}

#if UNITY_EDITOR

		[ContextMenu("Generate DB")]
		private void Editor_GenerateDB()
		{
			GenerateDB();
		}

#endif
	}
}
