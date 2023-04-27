using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float interactRange = .175f;

    private BoxCollider2D boxCollider;
   
    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();

        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        IInteractable interactble = GetInteractablObject();
        if(interactble != null) {
            interactble.Interact();
        }
    }

    private void Update() {
        HandleMovement();
    }
     
    public IInteractable GetInteractablObject() {
        List<IInteractable> interactableList = new List<IInteractable>();
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach(Collider2D collider in colliderArray) {
            if(collider.TryGetComponent(out IInteractable interactable)) {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractble = null;
        foreach(IInteractable interactable in interactableList) {
            if(closestInteractble == null) {
                closestInteractble = interactable;
            } else {
                if(Vector2.Distance(transform.position, interactable.GetTransform().position) <
                    Vector2.Distance(transform.position, closestInteractble.GetTransform().position)) {
                    //Closer
                    closestInteractble = interactable;
                }
            }
        }
        return closestInteractble;
    }
    
    private void HandleMovement() {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        float moveDistance = Time.deltaTime * moveSpeed;

        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0f);

        bool canMove = !Physics2D.BoxCast(transform.position, boxCollider.size, 0f, moveDir, moveDistance);

        if(!canMove) {
            // Can Move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics2D.BoxCast(transform.position, boxCollider.size, 0f, moveDirX, moveDistance);
            if(canMove) {
                // Can move only the X
                moveDir = moveDirX;
            } else {
                // Attempt only Y movement

                Vector3 moveDirY = new Vector3(0, moveDir.y, 0).normalized;
                canMove = !Physics2D.BoxCast(transform.position, boxCollider.size, 0f, moveDirY, moveDistance);

                if(canMove) {
                    // Can move only the Y
                    moveDir = moveDirY;
                } else {
                    // Cannot move any direction
                }
            }
        }
        if(canMove) {
            transform.position += moveDir * moveDistance;
        }

        if(inputVector.x > 0f) {
            // Rotate Player at the right side
            transform.localScale = Vector3.one;
        } 
        if(inputVector.x < 0f){
            // Rotate Player at the left side
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}