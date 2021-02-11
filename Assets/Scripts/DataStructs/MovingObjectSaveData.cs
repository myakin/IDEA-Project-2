using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingObjectSaveData {
    public Vector3 currentPosition;
    public float range;
    public float multiplier;
    public float speed;
    public Vector3 sPos;
    public Vector3 dir;
    public Vector3 ePos;

    public MovingObjectSaveData() {}

    public MovingObjectSaveData(Vector3 aCurrentPosition, float aRange, float aMultiplier, float aSpeed, Vector3 aStartPos, Vector3 aDirection, Vector3 anEndPos) {
        this.currentPosition = aCurrentPosition;
        this.range = aRange;
        this.multiplier = aMultiplier;
        this.speed = aSpeed;
        this.sPos = aStartPos;
        this.dir = aDirection;
        this.ePos = anEndPos;
    }
}
