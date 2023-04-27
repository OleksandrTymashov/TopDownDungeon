using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour {

    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI interactText;

    private void Update() {
        if(player.GetInteractablObject() != null) {
            Show(player.GetInteractablObject());
        } else { 
            Hide(); 
        }
    }
    private void Show(IInteractable interactble) { 
        containerGameObject.SetActive(true);
        interactText.text = interactble.GetInteractText();
    }

    private void Hide() {
        containerGameObject.SetActive(false);
    }
}