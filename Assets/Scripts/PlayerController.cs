using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // public float jumpStrength = 100f; // jump
    public Transform playerCam;
    
    private Animator animator; // jump
    // private bool isJumping, isRaycasting, isJumpForceApplied; // jump

    private float moveMultiplier = 1;
    
    // private Vector3 defaultColliderCenter, jumpingColliderCenter; // jump
    // private float defaultColliderHeight, jumpingColliderHeight; // jump
    
    private bool turnAroundModePlayerRotCorrected;
    

    private void Start() {
        animator = GetComponent<Animator>();

        // defaultColliderCenter = GetComponent<CapsuleCollider>().center;
        // defaultColliderHeight = GetComponent<CapsuleCollider>().height;
        // jumpingColliderCenter = new Vector3 (defaultColliderCenter.x, 1.37f, defaultColliderCenter.z);
        // jumpingColliderHeight = 1.19f;

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
        // if (jumpValue > 0 && !isJumping) {
        //     isJumping=true;
        //     animator.SetTrigger("jump");
        //     SetColliderForJumping();
        // }
        // if (isJumping) {
        //     if (!isJumpForceApplied && jumpValue > 0) {
        //         ApplyJumpForce();
        //     }
            
        //     if (isRaycasting) {
        //         PerformRaycast();
        //     }
        // }

        if (!playerCam.GetComponent<CameraFollow>().CheckIfTurnAroundModeOn()) { // turn around mode off
            transform.rotation *= Quaternion.Euler(0, mouseX, 0);
        } else { // turn around mode on
            playerCam.GetComponent<CameraFollow>().SetTurnAroundOffset(mouseX);
            playerCam.GetComponent<CameraFollow>().SetDistance(Input.mouseScrollDelta.y);
            if (ver!=0) {
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
        
    }

    // // called from animator
    // public void ResetJump() {
    //     isJumping = false;
    //     isRaycasting = false;
    //     isJumpForceApplied = false;
    //     GetComponent<Rigidbody>().velocity = Vector3.zero;
    //     totalUpForce = Vector3.zero;
    // }

    // // called from animator
    // public void ResetCollider() {
    //     GetComponent<CapsuleCollider>().center = defaultColliderCenter;
    //     GetComponent<CapsuleCollider>().height = defaultColliderHeight;
    // }

    // // called from animator
    // public void StartRaycast() {
    //     isRaycasting = true;
    // }
    // private void PerformRaycast() {
    //     RaycastHit hit;
    //     if (Physics.Raycast(transform.position, -transform.up, out hit, 0.5f, 1<<0, QueryTriggerInteraction.Ignore)) {
    //         isRaycasting = false;
    //         animator.SetTrigger("ProceedToLastJumpPhase");
    //     }
    // }

    // Vector3 totalUpForce = Vector3.zero;
    // private void ApplyJumpForce() {
    //     Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
    //     Vector3 forceHorizontal = currentVelocity * (jumpStrength * Time.deltaTime);
    //     Vector3 forceUp = transform.up * (jumpStrength * Time.deltaTime);
    //     GetComponent<Rigidbody>().AddForce(forceHorizontal + forceUp, ForceMode.Force);
    //     totalUpForce+=forceUp;
    // }

    // // called from animator
    // public void RistrictApplyingJumpForce() {
    //     isJumpForceApplied = true;
    // }
    // private void SetColliderForJumping(){
    //     GetComponent<CapsuleCollider>().center = jumpingColliderCenter;
    //     GetComponent<CapsuleCollider>().height = jumpingColliderHeight;
    // }

    // // called from animator
    // public void TestForShortJump() {
    //     // Debug.Log(totalUpForce.magnitude);
    //     if (totalUpForce.magnitude<2000) {
    //         animator.SetTrigger("ProceedToLastJumpPhase");
    //     }
    // }

    // private void OnCollisionEnter(Collision other) {
    //     Debug.Log(other.collider.tag);
    //     if (other.collider.tag=="Ground") {
    //         if (isJumping) {
    //             animator.SetTrigger("ProceedToLastJumpPhase");
    //         }
    //     }
    // }
}
