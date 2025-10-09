using UnityEngine;

public class ItemBehaviour : BaseInteractable
{
    [SerializeField] Item item;
    [SerializeField] Inventory playerInventory;

    public void GetObtained()
    {
        playerInventory.ObtainItem(item);
    }

    public override void ResetValues()
    {
        base.ResetValues();
        transform.position = item.spawnLocation;
    }
}
