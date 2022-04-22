using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public GameObject dialogueGUI;
    public Transform dialogueBoxGUI;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public KeyCode dialogueInput = KeyCode.F;

    public string Names;

    public string[] dialogueLines;

    public bool letterisMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outofRange = true;

    public AudioClip audioClip;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueText.text = "";

    }
    private void Update()
    {
        
    }

    public void EnterRangeofNPC()
    {
        outofRange = false;
        dialogueGUI.SetActive(true);
        if (dialogueActive == true)
        {
            dialogueGUI.SetActive(false);
        }

    }
    public void NPCName()
    {
        outofRange = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        }
        StartDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if (outofRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterisMultiplied)
            {
                if (!letterisMultiplied)
                {
                    letterisMultiplied = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (Input.GetKeyDown(dialogueInput) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }
    }

    private IEnumerator DisplayString (string stringToDisplay)
    {
        if (outofRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(dialogueInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F); 

                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }

            }
            while (true)
            {
                if (Input.GetKeyDown(dialogueInput))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterisMultiplied = false;
            dialogueText.text = "";
        }
    }

    public void DropDialogue()
    {
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
    }

    public void OutOfRange()
    {
        outofRange = true;
        if (outofRange == true)
        {
            letterisMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogueGUI.SetActive(false);
            dialogueBoxGUI.gameObject.SetActive(false);
        }
    }
}
