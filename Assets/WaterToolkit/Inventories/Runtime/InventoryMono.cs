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
            Inventory inventory = new Inventory();

            for(int i = 0; i < 10; i++)
            {
                InventoryItem itemObj = new InventoryItem();
				itemObj.name = $"Item{i}";
				itemObj.quantity = UnityEngine.Random.Range(3, 99);
				itemObj.category = "Common";
				itemObj.rarity = UnityEngine.Random.Range(0f, 1f);
                inventory.Add(itemObj);
            }

            string result = string.Empty;
            List<InventoryItem> itemObjs = inventory.Get(
                (InventoryItem obj) => obj.IsValid(), 5,
                (InventoryItem a, InventoryItem b) => new IntValueComparer().Compare(a.quantity, b.quantity));

            result += $"Inventory: {Environment.NewLine}";
            foreach(InventoryItem itemObj in itemObjs)
            {
                result += itemObj.ToString();
            }

            Debug.Log(result);
        }
    }
}
