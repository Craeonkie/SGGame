using UnityEngine;

public class NPCSystem : BaseInteractable
{
    [SerializeField] private NPCInfo npc;
    [SerializeField] private Inventory inventory;
    private DialogueManager dialogueManager;
    public bool isJoining = false;

    [Header("If they hold an item:")]
    [SerializeField] private bool _HoldingItem;
    [SerializeField] private Item _ItemHeld;

    private void Start()
    {
        dialogueManager = GameObject.Find("GameManager").GetComponent<DialogueManager>();
        ResetValues();

        //scuffed way to check if they have item or nah
        if (_ItemHeld == null)
        {
            _ItemHeld = null; 
        }
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
        if (_HoldingItem && !_ItemHeld.isCollected)
        {
            hasItem();
        }

        //if (_needsItem && inventory.HasItem(npc.requiredItem))
        //{
        //    npc.finishedQuest = true;
        //    whenInteractedWith.Invoke();
        //    isJoining = true;
        //}
    }

    public void hasItem()
    {
        isJoining = true;
        inventory.ObtainItem(_ItemHeld);
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
