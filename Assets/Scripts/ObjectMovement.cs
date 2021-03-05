using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {
    [Header("Editor Parameters")]
    public float moveRange = 8;
    public float moveSpeed = 0.01f;

    [Header("Set at runtime by system")]
    public Vector3 startPos;
    public Vector3 endPos;
    public float moveMultiplier = 1;
    private float sqrMoveRange;

    [HideInInspector] public Vector3 moveDir = new Vector3(1, 0, 0);


    private void Start() {
        startPos = transform.position;
        endPos = startPos + (transform.forward * moveRange);
        sqrMoveRange = moveRange * moveRange;
    }

    // Update is called once per frame
    void Update() {
        if ((transform.position - startPos).sqrMagnitude > sqrMoveRange || (transform.position - endPos).sqrMagnitude > sqrMoveRange) {
            moveMultiplier *= -1;
        }
        transform.position += transform.forward * (moveSpeed * moveMultiplier);

    }

    public void SetEnemyData(Vector3 aCurrentPosition, float aRange, float aMultiplier, float aMoveSpeed, Vector3 aStartPos, Vector3 anEndPos) {
        moveRange = aRange;
        moveMultiplier = aMultiplier;
        moveSpeed = aMoveSpeed;
        startPos = aStartPos;
        endPos = anEndPos;
        transform.position = aCurrentPosition;
    }
}
