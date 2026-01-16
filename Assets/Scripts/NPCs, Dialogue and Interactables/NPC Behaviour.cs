using UnityEngine;

public class NPCSystem : BaseInteractable
{
    [SerializeField] private NPCInfo npc;
    [SerializeField] private Inventory inventory;
    private DialogueManager dialogueManager;
    public bool isJoining = false;

    [Header("If they hold an item")]
    [SerializeField] private bool _HoldingItem;
    [SerializeField] private Item _itemHeld;

    private void Start()
    {
        dialogueManager = GameObject.Find("GameManager").GetComponent<DialogueManager>();
        ResetValues();

        //scuffed way to check if they have item or nah
        if (_itemHeld == null)
            _itemHeld = null;
    }

    private void Update()
    {
        //if they havent been invited:
        if (!isJoining)
        {
            // Display exclamation mark

        }
    }

    public override void GetInteractedWith()
    {
        if (!interacted)
        {
            interacted = true;
            whenInteractedWith.Invoke();
        }
        else if (interacted && npc.hasQuest && !npc.finishedQuest)
        {
            if (inventory.HasItem(npc.requiredItem))
            {
                npc.finishedQuest = true;
                whenInteractedWith.Invoke();
                isJoining = true;
            }
        }

        //if the npc is holding an item
        if (_HoldingItem)
        {
            GiveItem();
        }

        //if (_needsItem && inventory.HasItem(npc.requiredItem))
        //{
        //    npc.finishedQuest = true;
        //    whenInteractedWith.Invoke();
        //    isJoining = true;
        //}
    }

    public void GiveItem()
    {
        isJoining = true;
        inventory.ObtainItem(_itemHeld);
    }

    public void DoDialogue()
    {
        dialogueManager.EnterDialogue(npc);
    }

    public override void ResetValues()
    {
        base.ResetValues();
        npc.ResetValues();
        isJoining = false;
    }
}
