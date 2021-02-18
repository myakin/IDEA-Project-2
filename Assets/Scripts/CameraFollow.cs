using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float upOffset = 2.17f;
    public float backOffset = 2.3f;
    public float dampeningPos = 0.01f;
    public float dampeningRot = 0.05f;
    public float scrollSpeed = 0.1f;


    private float rotationOffsetUp;
    private bool isTurnAroundModeOn;
    private float turnAroundOffset;
    

    private void LateUpdate() {
        if (!isTurnAroundModeOn) {
            Vector3 targetPosition = target.position + target.up * upOffset + (-target.forward * backOffset);
            Quaternion targetRotation = target.rotation * Quaternion.Euler(17.42f + rotationOffsetUp, -4.36f, 0);

            transform.position = Vector3.LerpUnclamped(transform.position, targetPosition, dampeningPos);
            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, targetRotation, dampeningRot);
        
        } else {
            // Vector3 positionAtRadius = target.position + target.up * upOffset + (-target.forward * backOffset);
            // // Vector3 finalPosition = Quaternion.AngleAxis(turnAroundOffset, target.up) * positionAtRadius;
            // Vector3 finalPosition = Quaternion.Euler(0, turnAroundOffset, 0) * positionAtRadius;
            // transform.position = finalPosition;

            Vector3 offset = target.up * upOffset + (-target.forward * backOffset);
            // Vector3 finalPosition = Quaternion.AngleAxis(turnAroundOffset, target.up) * positionAtRadius;
            Vector3 finalPosition = target.position + (Quaternion.Euler(0, turnAroundOffset, 0) * offset);
            transform.position = finalPosition;

            transform.rotation = target.rotation * Quaternion.Euler(17.42f + rotationOffsetUp, -4.36f + turnAroundOffset, 0);
        }
    }

    public void SetRotationOffset(float anOffset) {
        rotationOffsetUp += anOffset;
    }

    public void ToggleCameraMode() {
        isTurnAroundModeOn = !isTurnAroundModeOn;
    }
    public bool CheckIfTurnAroundModeOn() {
        return isTurnAroundModeOn;
    }
    public void SetTurnAroundOffset(float anOffset) {
        turnAroundOffset += anOffset;
    }
    public float GetTurnAroundOffset() {
        return turnAroundOffset;
    }
    public void ResetTurnAroundOffset() {
        turnAroundOffset = 0;
    }
    public void SetDistance(float anOffset) {
        backOffset += anOffset * scrollSpeed;
    }


}
