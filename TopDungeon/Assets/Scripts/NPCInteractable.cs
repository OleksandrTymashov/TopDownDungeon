using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable {

    [SerializeField] private string interactText;
    [SerializeField] private bool isInteractable = true;

    public Dialogue dialogue;

    public void Interact() {
        if(isInteractable) {
            DialogueManager.Instance.StartDialogue(dialogue);
        }
    }

    public string GetInteractText() {
        return interactText;
    }

    public Transform GetTransform() {
        return transform;
    }

    public bool IsInteractable() {
        return isInteractable;
    }
}
