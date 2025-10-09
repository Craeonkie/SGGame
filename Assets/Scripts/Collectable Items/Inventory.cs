using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items;

    public void ObtainItem(Item item)
    {
        items.Add(item);
        foreach (var i in items)
        {
            i.isCollected = true;
        }
    }

    public bool HasItem(Item item)
    {
        return items.Exists(i => i.itemName == item.itemName);
    }

    public void ResetValues()
    {
        items.Clear();
        foreach (var i in items)
        {
            i.isCollected = false;
        }
    }
}
