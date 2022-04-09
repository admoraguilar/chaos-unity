using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProjectCHAOS.Common;

namespace ProjectCHAOS.Scores
{
	public class Scorer : MonoBehaviour
	{
		[SerializeField]
		private List<Score> _scoreList = new List<Score>();

		private string _dataStoreLocation
		{
			get {
				string path = Path.Combine(Application.persistentDataPath, "scores.json");
				return PathUtilities.GetPath(path);
			}
		}

		public Score GetScore(int id)
		{
			Score result = _scoreList.Find(s => s.id == id);
			if(result == null) {
				result = new Score(id);
				_scoreList.Add(result);
			}
			return result;
		}

		public void Load()
		{
			if(!File.Exists(_dataStoreLocation)) {
				Debug.LogWarning($"[{typeof(Scorer)}] No score data exists.");
				return;
			}

			string data = File.ReadAllText(_dataStoreLocation);
			_scoreList = JsonConvert.DeserializeObject<List<Score>>(data);
		}

		public void Save()
		{
			string data = JsonConvert.SerializeObject(_scoreList);
			File.WriteAllText(_dataStoreLocation, data);
		}

		public void Clear()
		{
			if(_scoreList == null) {
				_scoreList = new List<Score>();
			}

			_scoreList.Clear();
		}
	}
}
