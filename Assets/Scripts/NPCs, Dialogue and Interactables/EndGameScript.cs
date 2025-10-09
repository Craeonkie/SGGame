using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] private NPCSystem[] _npcs;
    [SerializeField] private Item[] _foodItems;

    [SerializeField] private int _foodCounter = 0;
    [SerializeField] private int _npcCounter = 0;
    [SerializeField] private Inventory _inventory;

    [Header("Dialogue")]
    [SerializeField] private List<endGameDialogue> dialogue;
    private int currentDialogueIndex;

    public bool ProgressCurrentDialogue()
    {
        if (currentDialogueIndex < dialogue.Count - 1)
        {
            currentDialogueIndex += 1;
            return true;
        }
        currentDialogueIndex = 0;
        return false;
    }

    public void initDialogue()
    {
        if (_npcCounter > _npcs.Length / 2)
        {
            addDialogueLine("Ma: Wow, you got the whole kampong ah?");
            addDialogueLine("Ma: Good job!!");
        }
        else
        {
            addDialogueLine("Ma: Mm... Not a lot of people leh..");
            addDialogueLine("[Your mom sighs out in disappointment.]");
        }

        if (_foodCounter > _foodItems.Length / 2)
            addDialogueLine("Ma: I managed to cook everything I needed to!");
        else
            addDialogueLine("Ma: Wah.. I didn't get to cook what they like..");

        addDialogueLine("[You sat with your friends, chatting and playing.]");
        addDialogueLine("Everyone: Happy National Day!!");

    }

    public void ResetValues()
    {
        currentDialogueIndex = 0;
        _foodCounter = 0;
        _npcCounter = 0;
    }

    //add a line in code
    public void addDialogueLine(string line)
    {
        dialogue.Add(new endGameDialogue { dialogue = line});
    }

    public endGameDialogue ReturnCurrentDialogue()
    {
        return dialogue[currentDialogueIndex];
    }
    public void countNPCS()
    {
        for (int i = 0; i < _npcs.Length; i++)
        {
            if (_npcs[i].isJoining)
                _npcCounter++;
        }
    }
    public void countFood()
    {
        foreach (Item i in _inventory.items)
        {
            _foodCounter++;
        }
    }
}

[System.Serializable]
public struct endGameDialogue
{
    public string dialogue;
}
