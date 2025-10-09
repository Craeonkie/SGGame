using UnityEngine;

public class ItemBehaviour : BaseInteractable
{
    [SerializeField] Item item;
    [SerializeField] Inventory playerInventory;

    public void GetObtained()
    {
        playerInventory.ObtainItem(item);
        item.isCollected = true;
    }

    public override void ResetValues()
    {
        base.ResetValues();
        transform.position = item.spawnLocation;
        item.isCollected = false;
    }
}
