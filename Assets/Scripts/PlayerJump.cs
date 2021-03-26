using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public float jumpStrength = 100f;
    private Animator animator;
    private bool isJumping, isRaycasting, isJumpForceApplied;
    private Vector3 defaultColliderCenter, jumpingColliderCenter; // jump
    private float defaultColliderHeight, jumpingColliderHeight; // jump
    private Vector3 totalUpForce = Vector3.zero;
    private float jumpValue;
    

    public void OnJump(InputAction.CallbackContext value) {
        jumpValue = value.ReadValue<float>();
    }

    private void Start() {
        animator = GetComponent<Animator>();

        defaultColliderCenter = GetComponent<CapsuleCollider>().center;
        defaultColliderHeight = GetComponent<CapsuleCollider>().height;
        jumpingColliderCenter = new Vector3 (defaultColliderCenter.x, 1.37f, defaultColliderCenter.z);
        jumpingColliderHeight = 1.19f;

    }

    // Update is called once per frame
    void Update()
    {

        if (jumpValue > 0 && !isJumping) {
            isJumping=true;
            animator.SetTrigger("jump");
            SetColliderForJumping();
            transform.SetParent(null);
        }
        if (isJumping) {
            if (!isJumpForceApplied && jumpValue > 0) {
                ApplyJumpForce();
            }
            
            if (isRaycasting) {
                PerformRaycast();
            }
        }
        
    }


    // called from animator
    public void ResetJump() {
        isJumping = false;
        isRaycasting = false;
        isJumpForceApplied = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        totalUpForce = Vector3.zero;
    }

    // called from animator
    public void ResetCollider() {
        GetComponent<CapsuleCollider>().center = defaultColliderCenter;
        GetComponent<CapsuleCollider>().height = defaultColliderHeight;
    }

    // called from animator
    public void StartRaycast() {
        isRaycasting = true;
    }
    private void PerformRaycast() {
        // RaycastHit hit;
        // if (Physics.Raycast(transform.position, -transform.up, out hit, 0.5f, 1<<0, QueryTriggerInteraction.Ignore)) {
        //     isRaycasting = false;
        //     animator.SetTrigger("ProceedToLastJumpPhase");
        // }

        GetComponent<ObjectRaycaster>().PerformRaycast(
            transform.position, 
            -transform.up, 
            0.5f, 
            1<<0, 
            QueryTriggerInteraction.Ignore,
            delegate (RaycastHit hitObj) {
                Debug.Log(hitObj.collider.gameObject.name);
                isRaycasting = false;
                animator.SetTrigger("ProceedToLastJumpPhase");
                if (hitObj.transform.tag=="Platform") {
                    transform.SetParent(hitObj.transform);
                }
            }
        );
    }

    
    private void ApplyJumpForce() {
        Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;
        Vector3 forceHorizontal = currentVelocity * (jumpStrength * Time.deltaTime);
        Vector3 forceUp = transform.up * (jumpStrength * Time.deltaTime);
        GetComponent<Rigidbody>().AddForce(forceHorizontal + forceUp, ForceMode.Force);
        totalUpForce+=forceUp;
    }

    // called from animator
    public void RistrictApplyingJumpForce() {
        isJumpForceApplied = true;
    }
    private void SetColliderForJumping(){
        GetComponent<CapsuleCollider>().center = jumpingColliderCenter;
        GetComponent<CapsuleCollider>().height = jumpingColliderHeight;
    }

    // called from animator
    public void TestForShortJump() {
        // Debug.Log(totalUpForce.magnitude);
        if (totalUpForce.magnitude<2000) {
            animator.SetTrigger("ProceedToLastJumpPhase");
        }
    }

    public void IncreaseJumpStrength(float aValue) {
        jumpStrength+=aValue;
        DataManager.instance.SetJumpStrength(jumpStrength);
    }
}
