using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public Vector3 spawnLocation;
    public bool isFoodItem;
}
