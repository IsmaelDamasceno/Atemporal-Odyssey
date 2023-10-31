using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private float charDelay;

    private TextMeshProUGUI text;

    private int line = 0;
    private static string[] dialogueList;

    private static DialogueSystem instance;

    private static bool nextLine = false;
    private static bool typing = false;

    public static void InitDialogue(string[] dialogueList)
    {
        if (instance.gameObject.activeInHierarchy)
        {
            return;
        }

        instance.StopAllCoroutines();
        instance.line = 0;
        instance.gameObject.SetActive(true);
        DialogueSystem.dialogueList = dialogueList;
        instance.text.text = "";
        instance.StartCoroutine(instance.DialoguePlay());

        PropertiesCore.Player.GetComponent<PropertiesCore>().ChangeState(PlayerState.Dialogue);
    }

    public static void SkipDialogue(InputAction.CallbackContext ctx)
    {
        if (instance.gameObject.activeSelf)
        {
            if (typing)
            {
                instance.text.text = dialogueList[instance.line];
                nextLine = false;
                typing = false;
            }
            else
            {
                instance.line++;
                instance.text.text = "";
                nextLine = true;
            }
        }
    }

    void Start()
    {
        if (instance == null)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            instance = this;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        
    }

    private IEnumerator DialoguePlay()
    {
        do {
            nextLine = false;
            typing = true;
            foreach (char c in dialogueList[line])
            {
                if (nextLine || !typing)
                {
                    break;
                }
                instance.text.text += c;
                yield return new WaitForSeconds(charDelay);
            }
            typing = false;

            while(!nextLine)
            {
                Debug.Log("waiting for next line");
                yield return null;
            }
        } while (line < dialogueList.Length);
        instance.gameObject.SetActive(false);
        PropertiesCore.Player.GetComponent<PropertiesCore>().ChangeState(PlayerState.Free);
        instance.StopAllCoroutines();
        yield return null;
    }
}
