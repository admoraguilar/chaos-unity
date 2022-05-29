using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ProjectCHAOS.DataSerialization
{
	public static class JsonObjectVersion
	{
		private static string _logPrepend => $"[{nameof(JsonObjectVersion)}]";

		private static string _objectVersionKey = nameof(IObjectVersion.objectVersion);
		private static string _serializationTimestampKey = "__serializationTimestamp";

		private static CultureInfo _cultureInfo = CultureInfo.InvariantCulture;

		public static T Deserialize<T>(string objectVersionJson, JsonSerializerSettings settings = null) where T : IObjectVersion, new()
		{
			if(settings == null) {
				settings = CreateDefaultJsonSerializerSettings();
			}

			IObjectVersion result = new T();
			IObjectVersion latestSerialized = null;

			List<object> dataList = AsDataList(objectVersionJson, settings);
			Dictionary<int, object> dataLookup = ToDataLookup(dataList);

			DateTimeOffset transientLastSerializedDate = DateTimeOffset.MinValue;

			foreach(KeyValuePair<int, object> data in dataLookup) {
				// Disregard higher versions if we're on a lower one.
				// Like parsing from V2 if you have 4 versions of the
				// same data.
				// The reason for this is if we're on a lower version
				// we most likely don't have a reference to the 
				// higher version hence we can't really convert from it
				if(data.Key > result.objectVersion) { continue; }

				JObject dataJObj = JObject.FromObject(data.Value);

				int objectVersion = GetObjectIfExisting(_objectVersionKey, dataJObj, 1); ;
				if(objectVersion == result.objectVersion) {
					result = (T)dataJObj.ToObject(result.GetType());
				}

				DateTimeOffset lastSerializedDate = GetObjectIfExisting(_serializationTimestampKey, dataJObj, DateTimeOffset.MinValue);
				if(lastSerializedDate >= transientLastSerializedDate) {
					transientLastSerializedDate = lastSerializedDate;

					// NOTES:
					// This could be heavy if the data traversing is big,
					// but this could be evaluated in the future to better
					// get the type
					Type latestSerializedType = TraverseObjectVersion(result, objectVersion).GetType();
					latestSerialized = (IObjectVersion)dataJObj.ToObject(latestSerializedType);
				}
			}

			if(latestSerialized != null) {
				if(latestSerialized.objectVersion == result.objectVersion) {
					return (T)result;
				} else {
					return (T)TraverseObjectVersion(latestSerialized, result.objectVersion);
				}
			}

			return (T)result;
		}

		public static string Serialize(IObjectVersion data, string objectVersionJson = "", JsonSerializerSettings settings = null)
		{
			if(settings == null) {
				settings = CreateDefaultJsonSerializerSettings();
			}

			if(data.ToNext() != null) {
				throw new InvalidOperationException($"{_logPrepend} Data to serialize must be the latest!");
			}

			List<object> dataList = AsDataList(objectVersionJson, settings);
			Dictionary<int, object> dataLookup = ToDataLookup(dataList);

			do {
				JObject jObj = JObject.FromObject(data);
				jObj[_serializationTimestampKey] = DateTimeOffset.UtcNow.ToString(_cultureInfo.DateTimeFormat);

				dataLookup[data.objectVersion] = jObj;
				data = data.ToPrev();
			} while(data != null);

			//// Sorted but slow (use only on debugging)
			//IEnumerable<KeyValuePair<int, object>> sortedDataLookup = dataLookup.OrderBy((dl) => dl.Key);
			//return JsonConvert.SerializeObject(sortedDataLookup.Select(d => d.Value), formatting, GetJsonSerializerSettings());

			// Unordered but fast
			return JsonConvert.SerializeObject(dataLookup.Values, settings);
		}

		private static IObjectVersion TraverseObjectVersion(IObjectVersion root, int version)
		{
			bool isPrev = root.objectVersion > version;
			while(root.objectVersion != version) {
				if(isPrev) { root = root.ToPrev(); } 
				else { root = root.ToNext(); }
			}
			return root;
		}

		private static List<object> AsDataList(string json, JsonSerializerSettings settings)
		{
			List<object> dataList = new List<object>();
			if(!string.IsNullOrEmpty(json)) {
				if(!CanDeserialize(json, settings, out dataList)) {
					throw new InvalidDataException($"{_logPrepend} Source data to serialize on is invalid!");
				}
			}
			return dataList;
		}

		private static Dictionary<int, object> ToDataLookup(List<object> dataList)
		{
			Dictionary<int, object> dataLookup = new Dictionary<int, object>();
			foreach(object d in dataList) {
				JObject jObj = JObject.FromObject(d);
				int objectVersion = GetObjectIfExisting(_objectVersionKey, jObj, 1);
				dataLookup[objectVersion] = d;
			}
			return dataLookup;
		}

		private static T GetObjectIfExisting<T>(string key, JObject jObj, T @default)
		{
			if(jObj.ContainsKey(key)) { return jObj[key].ToObject<T>(); }
			else { return @default; }
		}

		public static bool CanDeserialize<T>(string json, JsonSerializerSettings settings, out T result)
		{
			CanDeserialize(json, typeof(T), settings, out object obj);
			return (result = (T)obj) != null;
		}

		private static bool CanDeserialize(string json, Type type, JsonSerializerSettings settings, out object result)
		{
			try {
				result = JsonConvert.DeserializeObject(json, type, settings);
				return true;
			} catch {
				result = default;
				return false;
			}
		}

		private static JsonSerializerSettings CreateDefaultJsonSerializerSettings()
		{
			return new JsonSerializerSettings {
				Formatting = Formatting.None,
				ContractResolver = new DefaultContractResolver() {
					IgnoreSerializableAttribute = false
				},
				ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
				DateParseHandling = DateParseHandling.DateTimeOffset,
				DateFormatString = _cultureInfo.DateTimeFormat.ToString(),
				Culture = _cultureInfo,
			};
		}
	}
}