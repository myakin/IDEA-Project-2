using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerCamera;
    private float moveMultiplier = 1;
    private bool isJumping = false;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveMultiplier = 3;
        } else {
            moveMultiplier = 1;
        }
        // if (Input.GetKey(KeyCode.W)) { // forward
        //     transform.position += transform.forward * 0.01f * moveMultiplier;
        // } else if (Input.GetKey(KeyCode.S)) { // backwards
        //     transform.position -= transform.forward * 0.01f * moveMultiplier;
        // }
        // if (Input.GetKey(KeyCode.A)) { // left
        //     transform.position += (-transform.right * 0.01f) * moveMultiplier;
        // } else if (Input.GetKey(KeyCode.D)) { // right
        //     transform.position += transform.right * 0.01f * moveMultiplier;
        // }

        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        if (ver != 0) {
            transform.position += transform.forward * 0.01f * ver * moveMultiplier;
        } 
        if (hor != 0) {
            transform.position += transform.right * 0.01f * hor * moveMultiplier;
        } 

        float jump = Input.GetAxis("Jump");
        if (jump>0 && !isJumping) {
            isJumping = true;
            GetComponent<Rigidbody>().AddForce(transform.up * 50000);
        }
        if (isJumping) {
            timer+=Time.deltaTime;
            if (timer>3) {
                isJumping = false;
                timer=0;
            }
        }

        // if (Input.GetKeyDown(KeyCode.Space)) { // jump
        //     // transform.position += transform.up * 1f;
        //     GetComponent<Rigidbody>().AddForce(transform.up * 500);
        // }


        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
        playerCamera.rotation *= Quaternion.Euler(-mouseY, 0, 0);
    }
}
