using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    const string IS_OPEN = "IsOpen";

    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject imageContainer;
    [SerializeField] private Image npcIcon;

    private Animator animator;

    private void Awake() {
        Hide();
    }

    private void Start() {
        animator = GetComponent<Animator>();

        dialogueManager.OnDialogueStart += DialogueManager_OnDialogueStart;
        dialogueManager.OnSentenceChanged += DialogueManager_OnSentenceChanged;
        dialogueManager.OnDialogueEnd += DialogueManager_OnDialogueEnd;
    }

    private void DialogueManager_OnDialogueEnd(object sender, System.EventArgs e) {
        animator.SetBool(IS_OPEN, false);
        Hide();
    }

    private void DialogueManager_OnSentenceChanged(object sender, DialogueManager.OnSentenceChangedEventArgs e) {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(e.sentence));
    }

    private void DialogueManager_OnDialogueStart(object sender, DialogueManager.OnDialogueStartEventArgs e) {
        Show();
        animator.SetBool(IS_OPEN, true);
        npcNameText.text =e.dialogue.name;
        npcIcon.sprite = e.dialogue.sprite;
        
    }

    private void Show() { 
        dialogueBox.gameObject.SetActive(true);
        imageContainer.gameObject.SetActive(true);
    } 
   private void Hide() {
        dialogueBox.gameObject.SetActive(false);
        imageContainer.gameObject.SetActive(false);
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
