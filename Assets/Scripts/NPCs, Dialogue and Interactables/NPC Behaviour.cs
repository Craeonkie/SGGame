using UnityEngine;

public class NPCSystem : BaseInteractable
{
    [SerializeField] private NPCInfo npc;
    [SerializeField] private Inventory inventory;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = GameObject.Find("GameManager").GetComponent<DialogueManager>();
    }

    private void Update()
    {
        // Display exclamation mark
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
            }
        }
    }

    public void DoDialogue()
    {
        dialogueManager.EnterDialogue(npc);
    }

    public override void ResetValues()
    {
        base.ResetValues();
        npc.ResetValues();
    }
}
