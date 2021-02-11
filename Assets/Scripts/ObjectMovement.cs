using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {
    [Header("Editor Parameters")]
    public float moveRange = 8;
    public float moveSpeed = 0.01f;
    public Vector3 moveDir = new Vector3(1, 0, 0);

    [Header("Set at runtime by system")]
    public Vector3 startPos;
    public Vector3 endPos;
    public float moveMultiplier = 1;


    private void Start() {
        startPos = transform.position;
        endPos = startPos + (moveDir * moveRange);
    }

    // Update is called once per frame
    void Update() {
        if ((transform.position - startPos).magnitude > moveRange || (transform.position - endPos).magnitude > moveRange) {
            moveMultiplier *= -1;
        }
        transform.position += moveDir * (moveSpeed * moveMultiplier);

    }

    public void SetEnemyData(Vector3 aCurrentPosition, float aRange, float aMultiplier, float aMoveSpeed, Vector3 aStartPos, Vector3 aDirection, Vector3 anEndPos) {
        moveRange = aRange;
        moveMultiplier = aMultiplier;
        moveSpeed = aMoveSpeed;
        startPos = aStartPos;
        moveDir = aDirection;
        endPos = anEndPos;
        transform.position = aCurrentPosition;
    }
}
