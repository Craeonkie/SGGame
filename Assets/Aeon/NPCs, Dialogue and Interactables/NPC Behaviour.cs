using UnityEngine;

public class NPCSystem : BaseInteractable
{
    [SerializeField] private NPCInfo npcInfo;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void DoDialogue()
    {

    }

    public override void ResetValues()
    {
        base.ResetValues();
        npcInfo.ResetValues();
    }
}
