using System;
using System.Collections.Generic;
using UnityEngine;
using WaterToolkit.GameDatabases;

namespace WaterToolkit.Inventories
{
    public class InventoryMono : MonoBehaviour
    {
        private void Start()
        {
            Run();
        }

        public void Run()
        {
            Inventory inventory = new Inventory
            {
                duplicateHandling = ItemDuplicateHandling.Override
            };

            for(int i = 0; i < 10; i++)
            {
                ItemObject itemObj = new ItemObject();
				itemObj.name = $"Item{i}";
				itemObj.quantity = UnityEngine.Random.Range(3, 99);
				itemObj.category = "Common";
				itemObj.rarity = UnityEngine.Random.Range(0f, 1f);
                inventory.Add(itemObj);
            }

            string result = string.Empty;
            List<ItemObject> itemObjs = inventory.Get(
                (ItemObject obj) => obj.IsValid(), 5,
                (ItemObject a, ItemObject b) => new IntValueComparer().Compare(a.quantity, b.quantity));

            result += $"Inventory: {Environment.NewLine}";
            foreach(ItemObject itemObj in itemObjs)
            {
                result += itemObj.ToString();
            }

            Debug.Log(result);
        }
    }
}
