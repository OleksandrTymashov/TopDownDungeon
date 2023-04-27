using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

    [SerializeField] private Transform lookAtPlayer;
    [SerializeField] private float boundX = 0.15f;
    [SerializeField] private float boundY = 0.05f;

    private void LateUpdate() {

        Vector3 delta = Vector3.zero;
        
        // Check if player inside the bounds on the X axis
        float deltaX = lookAtPlayer.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX) { 
            if (transform.position.x < lookAtPlayer.position.x) {
                delta.x = deltaX - boundX;
            }
            else {
                delta.x = deltaX + boundX;
            }
        }
        // Check if player inside the bounds on the X axis
        float deltaY = lookAtPlayer.position.y - transform.position.y;
        if(deltaY > boundY || deltaY < -boundY) {
            if(transform.position.y < lookAtPlayer.position.y) {
                delta.y = deltaY - boundY;
            } else {
                delta.y = deltaY + boundY;
            }
        }
        transform.position += new Vector3(delta.x, delta.y, 0);
    }

}