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
    [SerializeField] private ItemInfo[] _foodItems;

    [SerializeField] private int _foodCounter = 0;
    [SerializeField] private int _npcCounter = 0;
    [SerializeField] private Inventory _inventory;

    [Header("Dialogue")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text menu;
    [SerializeField] private float typingSpeed = 0.05f; // time between letters
    private List<Dialogue> dialogue;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private int currentDialogueIndex = 0;

    [Header("Game End Cutscene")]
    public UnityEvent triggerEnd;
    public UnityEvent playEndVideo;
    public UnityEvent displayStatScreen;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject videoObject;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject defaultScreen;
    [SerializeField] private GameObject loseScreen;
    private bool madeItHome = false;
    private bool canPlayFullEnd = false;

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        dialogue = new List<Dialogue>();
    }

    public void EnterHome()
    {
        madeItHome = true;
    }

    public void TriggerEnd()
    {
        triggerEnd.Invoke();
        CountFood();
        CountNPCS();

        if (!madeItHome)
        {
            TallyResults();
        }
        else
        {
            bool npc;
            bool food;

            if (_npcCounter > _npcs.Length / 2)
            {
                npc = true;
            }
            else
            {
                npc = false;
            }

            if (_foodCounter > _foodItems.Length / 2)
            {
                food = true;
            }
            else
            {
                food = false;
            }

            if (npc && food)
            {
                canPlayFullEnd = true;
            }

            DisplayEndText();
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
        foreach (Item item in _inventory.items)
        {
            if (item.itemInfo.isFoodItem)
            {
                _foodCounter++;
            }
        }
    }

    public void TallyResults()
    {
        menu.text = "Friends Invited: " + _npcCounter.ToString() + "\nIngredients Collected: " + _foodCounter.ToString();
        displayStatScreen.Invoke();

        // Background
        winScreen.SetActive(false);
        defaultScreen.SetActive(false);
        loseScreen.SetActive(false);
        if (canPlayFullEnd)
        {
            winScreen.SetActive(true);
        }
        else if (_npcCounter > _npcs.Length / 2 || _foodCounter > _foodItems.Length / 2)
        {
            defaultScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }
    }

    public void ResetValues()
    {
        dialogue.Clear();
        currentDialogueIndex = 0;
        _foodCounter = 0;
        _npcCounter = 0;
        canPlayFullEnd = false;
        madeItHome = false;
    }

    public void Temp()
    {
        playEndVideo.Invoke();
    }

    public void DisplayEndText()
    {
        InitDialogue();
        EnterDialogue();
    }

    private void OnVideoFinished(VideoPlayer source)
    {
        videoObject.SetActive(false);
        TallyResults();
    }

    //// Dialogue stuff
    // Initialize dialogue for the dialogue screen to display
    public void InitDialogue()
    {
        if (_npcCounter > _npcs.Length / 2)
        {
            dialogue.Add(new Dialogue("Wow, you got so many people ah?", true));
            dialogue.Add(new Dialogue("Good job!!", true));
        }
        else if (_npcCounter > 0)
        {
            dialogue.Add(new Dialogue("Mm... not a lot of people leh..", true));
            dialogue.Add(new Dialogue("[Your mom sighs out in disappointment.]", true));
        }
        else
        {
            dialogue.Add(new Dialogue("Ah Boy...You didn't even invite a single person...", true));
            dialogue.Add(new Dialogue("What were you doing the whole day ah?!", true));
        }
        
        if (_npcCounter == 0)
        {
            dialogue.Add(new Dialogue("...", true));
        }

        if (_foodCounter > _foodItems.Length / 2)
        {
            dialogue.Add(new Dialogue("...I managed to cook everything I needed to!", true));
            if (_npcCounter == 0)
            {
                dialogue.Add(new Dialogue("...but who are we cooking for ah...", true));
            }
        }
        else if (_foodCounter > 0)
        {
            dialogue.Add(new Dialogue("...Aiyah.. I didn't get to cook everything they like..", true));
        }
        else
        {
            dialogue.Add(new Dialogue("...Ah Boy, did you forget to bring the ingredients?", true));
        }

        if (_npcCounter > 0)
        {
            dialogue.Add(new Dialogue("[You sit with your friends, chatting and playing.]", false));
            dialogue.Add(new Dialogue("Happy National Day!!", false));
        }
    }

    private void EnterDialogue()
    {
        ShowLine();
    }

    private void ShowLine()
    {
        if (currentDialogueIndex == (dialogue.Count - 1))
        {
            if (_npcCounter == 0)
            {
                nameText.text = "Mom";
            }
            else
            {
                nameText.text = "Everyone";
            }
        }
        else if (dialogue[currentDialogueIndex].isNPCSpeaking)
        {
            nameText.text = "Mom";
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
            if (currentDialogueIndex == dialogue.Count - 1)
            {
                if (canPlayFullEnd)
                {
                    playEndVideo.Invoke();
                }
                else
                {
                    TallyResults();
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
