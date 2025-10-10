using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI NPCName;
    [SerializeField] private string playerName;
    [SerializeField] private Inventory inventory;
    [SerializeField] private float typingSpeed = 0.05f; // time between letters
    [SerializeField] private UnityEvent beginDialogue;
    [SerializeField] private UnityEvent endDialogue;
    private NPCInfo currentNPC;
    private Coroutine typingCoroutine;
    
    public void EnterDialogue(NPCInfo NpcInfo)
    {
        currentNPC = NpcInfo;
        beginDialogue.Invoke();
        ShowLine();
    }

    public void ShowLine()
    {
        if (currentNPC.ReturnCurrentDialogue().isNPCTalking)
        {
            NPCName.text = currentNPC.NPCName;
        }
        else
        {
            NPCName.text = playerName;
        }
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(currentNPC.ReturnCurrentDialogue().dialogue));
    }

    IEnumerator TypeText(string line)
    {
        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void PlayerClicksOnScreen()
    {
        if (!currentNPC.ProgressCurrentDialogue())
        {
            endDialogue.Invoke();
        }
        else
        {
            ShowLine();
        }
    }
}
