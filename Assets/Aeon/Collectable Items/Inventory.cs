using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items;

    public void ObtainItem(Item item)
    {
        items.Add(item);
    }

    public void ResetValues()
    {
        items.Clear();
    }
}
