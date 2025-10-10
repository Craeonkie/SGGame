using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] private NPCSystem[] _npcs;
    [SerializeField] private Item[] _foodItems;

    [SerializeField] private int _foodCounter = 0;
    [SerializeField] private int _npcCounter = 0;
    [SerializeField] private Inventory _inventory;

    [Header("Dialogue")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingSpeed = 0.05f; // time between letters
    private List<Dialogue> dialogue;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private int currentDialogueIndex = 0;

    [Header("Game End Cutscene")]
    public UnityEvent playEnding;
    public UnityEvent playFullEnd;
    public UnityEvent displayStatScreen;
    private bool madeItHome = false;
    private bool canPlayFullEnd = false;

    public void TriggerEnd()
    {
        if (!madeItHome)
        {
            displayStatScreen.Invoke();
        }
        else
        {
            //if ()
            //{
            //    canPlayFullEnd = true;
            //}
            PlayEnding();
        }
    }

    public void CountNPCS()
    {
        foreach (NPCSystem npc in _npcs)
        {
            if (npc.isJoining)
            {
                _npcCounter++;
            }
        }
    }
    public void CountFood()
    {
        _foodCounter = _inventory.items.Count;
    }

    public void InitDialogue()
    {
        if (_npcCounter > _npcs.Length / 2)
        {
            dialogue.Add(new Dialogue("Wow, you got the whole kampong ah?", true));
            dialogue.Add(new Dialogue("Good job!!", true));
        }
        else
        {
            dialogue.Add(new Dialogue("Mm... Not a lot of people leh..", true));
            dialogue.Add(new Dialogue("[Your mom sighs out in disappointment.]", true));
        }

        if (_foodCounter > _foodItems.Length / 2)
        {
            dialogue.Add(new Dialogue("...I managed to cook everything I needed to!", true));
        }
        else
        {
            dialogue.Add(new Dialogue("Wah.. I didn't get to cook what they like..", true));
        }

        dialogue.Add(new Dialogue("[You sit with your friends, chatting and playing.]", false));
        dialogue.Add(new Dialogue("Everyone: Happy National Day!!", false));

    }

    public void ResetValues()
    {
        dialogue.Clear();
        currentDialogueIndex = 0;
        _foodCounter = 0;
        _npcCounter = 0;
        canPlayFullEnd = false;
    }

    public void DisplayScoreboard()
    {

    }

    public void PlayEnding()
    {
        playEnding.Invoke();
        CountFood();
        CountNPCS();
        InitDialogue();
        EnterDialogue();
    }

    private void EnterDialogue()
    {
        ShowLine();
    }

    private void ShowLine()
    {
        if (dialogue[currentDialogueIndex].isNPCSpeaking)
        {
            nameText.text = "Mom";
        }
        else if (currentDialogueIndex == 7)
        {
            nameText.text = "Everyone";
        }
        else
        {
            nameText.text = "Ah boy";
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(dialogue[currentDialogueIndex].dialogue));
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

    public void PlayerClicksOnEndGameScreen()
    {
        if (isTyping)
        {
            // if mid-typing, instantly finish the line
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogue[currentDialogueIndex].dialogue;
            isTyping = false;
        }
        else
        {
            if (currentDialogueIndex >= 7)
            {
                if (canPlayFullEnd)
                {
                    playFullEnd.Invoke();
                }
                else
                {
                    displayStatScreen.Invoke();
                }
            }
            // otherwise, go to next dialogue
            else
            {
                currentDialogueIndex++;
                ShowLine();
            }
        }
    }
}
