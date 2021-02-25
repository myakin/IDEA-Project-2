using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour {
    public Vector3 crouchColliderCenter;
    public float crouchColliderHeight;

    private Vector3 defaultColliderCenter;
    private float defaultColliderHeight;
    private bool isCrouching;
    
    private void Start() {
        defaultColliderCenter = GetComponent<CapsuleCollider>().center;
        defaultColliderHeight = GetComponent<CapsuleCollider>().height;
    }

    void Update() {
        if (Input.GetKey(KeyCode.C)) {
            if (!isCrouching) {
                isCrouching = true;
                // GetComponent<Animator>().SetBool("isCrouching", true);
                GetComponent<CapsuleCollider>().center = crouchColliderCenter;
                GetComponent<CapsuleCollider>().height = crouchColliderHeight;
            }
            
        } else {
            if (isCrouching) {
                isCrouching = false;
                // GetComponent<Animator>().SetBool("isCrouching", false);
                GetComponent<CapsuleCollider>().center = defaultColliderCenter;
                GetComponent<CapsuleCollider>().height = defaultColliderHeight;
            }
        }
    }

}
