using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace WaterToolkit.DataSerialization
{
	public class DataSerializer
	{
		private string _folderRootPath = string.Empty;
		private string _subFolderRootPath = string.Empty;
		private string _fileName = string.Empty;

		public string folderRootPath
		{
			get {
				return !string.IsNullOrEmpty(_folderRootPath) ? _folderRootPath :
					Application.persistentDataPath;
			}
			private set => _folderRootPath = value;
		}

		public string subFolderRootPath
		{
			get {
				return !string.IsNullOrEmpty(_subFolderRootPath) ? _subFolderRootPath :
					"SaveData";
			}
			private set => _subFolderRootPath = value;
		}

		public string fileName
		{
			get {
				string result = !string.IsNullOrEmpty(_fileName) ? _fileName :
					"save.json";
				if(!result.EndsWith(".json")) {
					result += ".json";
				}
				return result;
			}
			private set => _fileName = value;
		}

		private string fullPath
		{
			get {
				string path = Path.Combine(folderRootPath, subFolderRootPath, fileName);
				return PathUtilities.GetPath(path);
			}
		}

		public DataSerializer(string fileName)
		{
			Initialize(string.Empty, string.Empty, fileName);
		}

		public DataSerializer(string subFolderRootPath, string fileName)
		{
			Initialize(string.Empty, subFolderRootPath, fileName);
		}

		public DataSerializer(
			string folderRootPath, string subFolderRootPath, 
			string fileName)
		{
			Initialize(folderRootPath, subFolderRootPath, fileName);
		}

		private void Initialize(
			string folderRootPath, string subFolderRootPath,
			string fileName)
		{
			this.folderRootPath = folderRootPath;
			this.subFolderRootPath = subFolderRootPath;
			this.fileName = fileName;
		}

		public string LoadRaw()
		{
			if(!File.Exists(fullPath)) {
				Debug.LogWarning($"No save data exists.");
				return string.Empty;
			}

			string data = File.ReadAllText(fullPath);
			return data;
		}

		public T LoadJson<T>() where T : new()
		{
			if(!File.Exists(fullPath)) {
				Debug.LogWarning($"No save data exists.");
				return new T();
			}

			string data = File.ReadAllText(fullPath);
			return JsonConvert.DeserializeObject<T>(data);
		}

		public T LoadObjectVersion<T>() where T : IObjectVersion, new()
		{
			if(!File.Exists(fullPath)) {
				Debug.LogWarning($"No save data exists.");
				return new T();
			}

			string data = File.ReadAllText(fullPath);
			return JsonObjectVersion.Deserialize<T>(data);
		}

		public void SaveRaw(object value)
		{
			string data = value.ToString();
			File.WriteAllText(fullPath, data);
		}

		public void SaveJson(object value)
		{
			string data = JsonConvert.SerializeObject(value);
			File.WriteAllText(fullPath, data);
		}

		public void SaveObjectVersion(IObjectVersion objectVersion)
		{
			string data = JsonObjectVersion.Serialize(objectVersion);
			File.WriteAllText(fullPath, data);
		}

		public void Clear()
		{
			if(File.Exists(fullPath)) {
				File.Delete(fullPath);
				Debug.Log("Save data successfully deleted");
			}
		}
	}
}
