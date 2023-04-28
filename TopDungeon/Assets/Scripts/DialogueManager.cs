using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    public event EventHandler<OnDialogueStartEventArgs> OnDialogueStart;
    public class OnDialogueStartEventArgs : EventArgs {
        public Dialogue dialogue;
    }
    public event EventHandler<OnSentenceChangedEventArgs> OnSentenceChanged;
    public class OnSentenceChangedEventArgs : EventArgs {
        public string sentence;
    }
    public event EventHandler OnDialogueEnd;
    public static DialogueManager Instance { get; private set; }

    private Queue<string> sentences;

    public void Awake() {
        Instance= this;
    }

    void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {


    
        sentences.Clear();
        OnDialogueStart?.Invoke(this, new OnDialogueStartEventArgs {
            dialogue = dialogue
        });

        foreach(string sentence in dialogue.sentences) {

            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {

        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        OnSentenceChanged?.Invoke(this, new OnSentenceChangedEventArgs {
            sentence = sentence
        });
    }

    private void EndDialogue() {
        OnDialogueEnd?.Invoke(this, EventArgs.Empty);
    }

}