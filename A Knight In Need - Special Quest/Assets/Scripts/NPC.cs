using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Transform chatBackground;
    public Transform npcCharacter;

    private DialogueSystem dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    private void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();

    }

    private void Update()
    {
        chatBackground.position = Camera.main.WorldToScreenPoint(npcCharacter.position + Vector3.up * 7f);
        Vector3 Pos = Camera.main.WorldToScreenPoint(npcCharacter.position);
        Pos.y += 175;
        chatBackground.position = Pos;
    }

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<NPC>().enabled = true;
        FindObjectOfType<DialogueSystem>().EnterRangeofNPC();
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.F))
        {
            this.gameObject.GetComponent<NPC>().enabled.true;
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            FindObjectOfType<DialogueSystem>().NPCName();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}
