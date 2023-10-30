using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private float charDelay;

    private TextMeshProUGUI text;

    private int line = 0;
    private static string[] dialogueList;

    private static DialogueSystem instance;

    public static void InitDialogue(string[] dialogueList)
    {
        DialogueSystem.dialogueList = dialogueList;
        instance.text.text = "";
    }

    void Start()
    {
        if (instance == null)
        {
            text = GetComponent<TextMeshProUGUI>();
            instance = this;
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
        foreach (char c in dialogueList[line])
        {


            yield return new WaitForSeconds(charDelay);
        }
    }
}
