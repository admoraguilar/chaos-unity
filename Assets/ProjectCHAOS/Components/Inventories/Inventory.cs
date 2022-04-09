using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCHAOS.Inventories
{
	public class InventoryMono : MonoBehaviour
	{
		[SerializeField]
		private Inventory _inventory = new Inventory();

		public Inventory inventory => _inventory;
	}

	[Serializable]
	public class Inventory
	{
		private List<Item> _items = new List<Item>();

		public IReadOnlyList<Item> items => _items;

		public bool TryGetItem(int id, out Item item)
		{
			item = _items.Find(i => i.id == id);
			return item != null;
		}

		public bool TryGetItem(string name, out Item item)
		{
			item = _items.Find(i => i.name == name);
			return item != null;
		}

		public void AddItem(Item item)
		{
			if(TryGetItem(item.name, out item)) {

			}
		}
	}

	public class Item
	{
		public int id;
		public string name;
		public string type;
		public int quantity;
	}
}
