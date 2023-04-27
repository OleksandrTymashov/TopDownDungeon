using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable {

    [SerializeField] private Sprite emptyChestSprite;
    [SerializeField] private SpriteRenderer chestGameObject;
    [SerializeField] private int pesosAmount;
    [SerializeField] private string interactText;

    bool collectable = true;

    private void CollectChest() {
        if(collectable) {
            collectable = false;
            chestGameObject.GetComponent<SpriteRenderer>().sprite = emptyChestSprite;
            Debug.Log("Grant " + pesosAmount + " pesos!");
        }
    }

    public void Interact() {
        CollectChest();
    }
    public string GetInteractText() {
        return interactText;
    }
    public Transform GetTransform() {
        return transform;
    }

}
