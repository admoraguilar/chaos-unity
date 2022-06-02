using System;
using System.Collections.Generic;
using UnityEngine;
using WaterToolkit.ValueBoards;

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
                duplicateHandling = ValueBoardDuplicateHandling.Override
            };

            for(int i = 0; i < 10; i++)
            {
                ItemObject itemObj = new ItemObject($"{UnityEngine.Random.Range(0, 99999)}");
                itemObj.AddValue(ItemObject.nameKey, $"Item{i}");
                itemObj.AddValue(ItemObject.quantityKey, UnityEngine.Random.Range(3, 99));
                itemObj.AddValue(ItemObject.categoryKey, "Common");
                itemObj.AddValue(ItemObject.rarityKey, UnityEngine.Random.Range(0f, 1f));
                inventory.Add(itemObj);
            }

            string result = string.Empty;
            List<ItemObject> itemObjs = inventory.Get(
                (ItemObject obj) => obj.IsValid(), 5,
                new ValueObjectIntValueComparer<ItemObject>(ItemObject.quantityKey));

            result += $"Inventory: {Environment.NewLine}";
            foreach(ItemObject itemObj in itemObjs)
            {
                result += itemObj.ToString();
            }

            Debug.Log(result);
        }
    }
}
