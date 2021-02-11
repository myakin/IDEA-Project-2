using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnimationController : MonoBehaviour
{
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
           
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) { // up animation trigger
            animator.SetTrigger("SwitchToUp");
        }
        if (Input.GetKeyDown(KeyCode.D)) { // side animation trigger
            animator.SetTrigger("SwitchToSide");
        }
    }
}
