using ProjectCHAOS.DataSerialization;

namespace ProjectCHAOS.GameSerialization
{
	public abstract class GameDataSerializer
	{
		private DataSerializer _dataSerializer = null;

		protected DataSerializer dataSerializer => _dataSerializer;

		public GameDataSerializer(string subFolderPath, string fileName)
		{
			Initialize(subFolderPath, fileName);
		}

		private void Initialize(string subFolderPath, string fileName)
		{
			_dataSerializer = new DataSerializer(subFolderPath, fileName);
		}

		public abstract void Load();

		public abstract void Save();

		public virtual void Clear()
		{
			_dataSerializer.Clear();
		}
	}
}
