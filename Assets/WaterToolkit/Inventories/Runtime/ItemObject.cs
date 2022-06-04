using System;
using UnityEngine;

namespace WaterToolkit.Inventories
{
	[Serializable]
    public class ItemObject
	{
		public string name = string.Empty;
		public int quantity = 0;
		public string category = string.Empty;
		public float rarity = 0f;
		public Sprite sprite = null;

		public bool IsValid()
        {
            return name != string.Empty && quantity >= 0 && 
				   category != string.Empty && rarity >= 0f;
        }
    }
}
