using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(DialogueRunner))]
public class DialogueSingleton : MonoBehaviour
{
    public static DialogueSingleton Instance;
    
    [HideInInspector]
    public DialogueRunner runner;

    void Awake()
    {
        Instance = this;
        runner = GetComponent<DialogueRunner>();
    }

    public void PlayNode(string nodeID)
    {
        runner.StartDialogue(nodeID);
    }
}
