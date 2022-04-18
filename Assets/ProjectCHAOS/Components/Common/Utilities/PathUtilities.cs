using System.IO;
using UnityEngine;

namespace ProjectCHAOS
{
	public class PathUtilities
	{
		public static string GetPath(string path)
		{
			string directory = path;
			if(Path.HasExtension(directory)) { directory = Path.GetDirectoryName(path); }
			if(!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
			return path;
		}
	}
}
