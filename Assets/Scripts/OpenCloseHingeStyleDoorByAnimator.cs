using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseHingeStyleDoorByAnimator : MonoBehaviour {
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            animator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            animator.SetTrigger("Close");
        }
    }

    public void OpenDoor() {
        animator.SetTrigger("Open");
    }
}
