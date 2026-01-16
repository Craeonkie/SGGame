using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    public void ObtainItem(Item item)
    {
        items.Add(item);
    }

    public bool HasItem(ItemInfo itemInfo)
    {
        foreach (Item item in items)
        {
            if (item.itemInfo == itemInfo)
            {
                return true;
            }
        }
        return false;
    }

    public void ResetValues()
    {
        items.Clear();
    }
}
