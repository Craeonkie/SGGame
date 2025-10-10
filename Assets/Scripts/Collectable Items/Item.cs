using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Vector3 spawnLocation;
    public bool isFoodItem;
    public bool isCollected;
    [Header("End of game points")]
    public int points;
}
