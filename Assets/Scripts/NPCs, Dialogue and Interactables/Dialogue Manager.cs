using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Inventory inventory;
    [SerializeField] private float typingSpeed = 0.05f; // time between letters
    [SerializeField] private UnityEvent beginDialogue;
    [SerializeField] private UnityEvent endDialogue;
    private NPCInfo currentNPC;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public void EnterDialogue(NPCInfo NpcInfo)
    {
        currentNPC = NpcInfo;
        beginDialogue.Invoke();
        ShowLine();
    }

    public void ShowLine()
    {
        if (currentNPC.ReturnCurrentDialogue().isNPCSpeaking)
        {
            nameText.text = currentNPC.NPCName;
        }
        else
        {
            nameText.text = "Ah boy";
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(currentNPC.ReturnCurrentDialogue().dialogue));
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;

        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void PlayerClicksOnScreen()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentNPC.ReturnCurrentDialogue().dialogue;
            isTyping = false;
        }
        else
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
}