using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 0.001f;
    public float jumpStrength = 100f;
    public float velocityForceMagnitude = 1;
    public Transform playerCam;
    
    
    private Animator animator;
    private bool isJumping;
    private bool isCheckingGround;
    private float jumpTimer;
    private Vector3 oldPosition;
    private float moveMultiplier = 1;
    private Vector2 defaultColliderOffset, defaultColliderSize, jumpingColliderOffset, jumpingColliderSize;
    private bool turnAroundModePlayerRotCorrected;

    private void Start() {
        animator = GetComponent<Animator>();
        // defaultColliderOffset = GetComponent<CapsuleCollider2D>().offset;
        // defaultColliderSize = GetComponent<CapsuleCollider2D>().size;

        // jumpingColliderOffset = new Vector2(defaultColliderOffset.x, 1.74f);
        // jumpingColliderSize = new Vector2(defaultColliderSize.x, 1.49f);

    }

    private void Update() {
        float hor  = Input.GetAxis("Horizontal");
        float ver  = Input.GetAxis("Vertical");
        float jumpValue = Input.GetAxis("Jump");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.Tab)) {
            playerCam.GetComponent<CameraFollow>().ToggleCameraMode();
        }

        

        moveMultiplier = Input.GetKey(KeyCode.LeftShift) ? moveMultiplier = 2 : moveMultiplier = 1;
        animator.SetFloat("vertical", ver * moveMultiplier);
        animator.SetFloat("horizontal", hor *  moveMultiplier);
        if (jumpValue > 0)
        {
            if (hor == 0 && ver == 0)
            {
                animator.SetBool("isStandingJump", true);
            }
            else
            {
                animator.SetBool("isStandingJump", false);
            }
            animator.SetTrigger("jump");
        }

        if (!playerCam.GetComponent<CameraFollow>().CheckIfTurnAroundModeOn()) { // turn around mode off
            transform.rotation *= Quaternion.Euler(0, mouseX, 0);
        } else { // turn around mode on
            playerCam.GetComponent<CameraFollow>().SetTurnAroundOffset(mouseX);
            playerCam.GetComponent<CameraFollow>().SetDistance(Input.mouseScrollDelta.y);
            if (ver!=0) {
                // if (!turnAroundModePlayerRotCorrected) {
                //     turnAroundModePlayerRotCorrected = true;
                //     float deltaAngle = Vector3.SignedAngle(-transform.forward, playerCam.position - transform.position, transform.up);
                //     transform.rotation *= Quaternion.Euler(0, deltaAngle, 0);
                //     playerCam.GetComponent<CameraFollow>().ResetTurnAroundOffset();
                // }
                // transform.rotation *= Quaternion.Euler(0, mouseX, 0);

                
                // unityde vektorlerin izdusumunu hesaplamak icin elimizdeki gibi bir duzenekte aslinda derste bahsettigim gibi cos almaya vs ihtiyacimiz yok
                // kameranin oyuncuya gore offsetUp'ini biliyoruz, ayrica kameranin pozisynunu biliyoruz. bu verilerle camera follow scriptinin izdusum vektorunu bize vermesini saglamak kolay
                Vector3 camPosOnPlayerPlane = playerCam.GetComponent<CameraFollow>().GetCameraPositionOnTargetPlane();
                Vector3 lookDirection = transform.position - camPosOnPlayerPlane;
                float deltaAngle = Vector3.SignedAngle(transform.forward, lookDirection, transform.up); // angle hesabini dogru yaparsak mantigin geri kalaninin isini dogru yapacagini dusunebiliriz

                // burada puf nokta su: her framede oyuncunun rotasyonunu deltaAngle ile degistirdim, artik kamera acisi ile senkronum (bakis acisiolarak)
                // ama kamera scriptindei turnAroundOffseti de ayni oranda dusurmem lazim. bunu yapmazsam surekli rotasyon yapan bir kamera acisi elde ediyorum. bunu yapinca kamera sabitlenebilmeli
                transform.rotation *= Quaternion.Euler(0, deltaAngle, 0);
                if (!turnAroundModePlayerRotCorrected) {
                    turnAroundModePlayerRotCorrected=true;
                    playerCam.GetComponent<CameraFollow>().ResetTurnAroundOffset();
                } else {
                    playerCam.GetComponent<CameraFollow>().DecreaseTurnAroundOffset(deltaAngle);
                }
                
                

            } else {
                turnAroundModePlayerRotCorrected= false;
            }
        }
        // playerCam.rotation *= Quaternion.Euler(-mouseY, 0, 0);
        playerCam.GetComponent<CameraFollow>().SetRotationOffset(-mouseY);


        if (ver!=0) {
            GetComponent<ObjectRaycaster>().PerformRaycast(
                delegate {
                    // Debug.Log("Hit ground while ver is not zero");
                }
            );
        }

        if (hor!=0) {
            GetComponent<ObjectRaycaster>().PerformRaycast(
                delegate {
                    // Debug.Log("Hit ground while hor is not zero");
                }
            );
        }

        // Debug.Log(jumpValue);

        // if (!isJumping) {
        //     if (Input.GetKey(KeyCode.LeftShift)) {
        //         moveMultiplier = 3;
        //         animator.SetBool("isRunning", true);
        //     } else {
        //         moveMultiplier = 1;
        //         animator.SetBool("isRunning", false);
        //     }
        //
        //     animator.SetFloat("walkLeftRight", hor * moveMultiplier);
        //     transform.position += transform.right * (moveSpeed * hor * moveMultiplier);
        //     
        //     if (hor>0) {
        //         GetComponent<SpriteRenderer>().flipX = false;
        //     } else if (hor<0) {
        //         GetComponent<SpriteRenderer>().flipX = true;
        //     }
        // }

        // // jump trigger
        // if (jumpValue>0) {
        //     if (!isJumping) {
        //         isJumping = true;
        //         animator.SetTrigger("triggerJump");
        //
        //         transform.SetParent(null);
        //
        //         Vector3 velocity = transform.position - oldPosition;
        //         GetComponent<Rigidbody2D>().AddForce(velocity * velocityForceMagnitude);
        //
        //         GetComponent<CapsuleCollider2D>().offset = jumpingColliderOffset;
        //         GetComponent<CapsuleCollider2D>().size = jumpingColliderSize;
        //
        //     }
        //     jumpTimer+=Time.deltaTime;
        //     if (jumpTimer<0.5f) {
        //         GetComponent<Rigidbody2D>().AddForce(transform.up * jumpStrength);
        //     }
        // } 

        // // ground checking
        // if (isCheckingGround) {
        //     RaycastGround();
        //
        //     // if (animator.GetCurrentAnimatorStateInfo(0).IsName("CharacterIdleAnimation")) {
        //     //     ResetJump();
        //     // }
        // }

        // oldPosition = transform.position;




    }

    // called from CharacterJumpEndAnimation animation
    public void ResetJump() {
        isCheckingGround = false;
        isJumping = false;
        animator.SetBool("isGrounded", false);
        jumpTimer = 0;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    // called from CharacterJumpStartAnimation animation
    public void StartCheckingGround() {
        isCheckingGround = true;
    }



    private void RaycastGround()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 0.2f, 1<<0);
        //if (hit.collider!=null && hit.collider.tag == "Platform") {
        //    // Debug.Log(hit.collider.gameObject.name);
        //    // set animator to idle
        //    animator.SetBool("isGrounded", true);
        //    transform.SetParent(hit.transform);
        //}

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.517f, 0.01f), 0, -transform.up, 0.2f, 1 << 0);
        if (hit.collider != null && hit.collider.tag == "Platform")
        {
            animator.SetBool("isGrounded", true);
            transform.SetParent(hit.transform);
        }
    }

    // called from animator
    public void ResetCollider() {
        GetComponent<CapsuleCollider2D>().offset = defaultColliderOffset;
        GetComponent<CapsuleCollider2D>().size = defaultColliderSize;
    }
}
