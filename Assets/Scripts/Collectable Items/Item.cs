using UnityEngine;

public class Item : BaseInteractable
{
    public ItemInfo itemInfo;
    [SerializeField] private Inventory playerInventory;

    public void GetObtained()
    {
        playerInventory.ObtainItem(this);
    }

    public override void ResetValues()
    {
        base.ResetValues();
        transform.position = itemInfo.spawnLocation;
    }
}
