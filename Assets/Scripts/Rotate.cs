using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 5;
    public float moveAmountZ = 1;
    public float maxDistance = 5;



    private float rotationAmountY = 0;
    private float currentPositionZ  = 0;
    private float directionMultiplier = 1;
    private Vector3 startPos;
    private float currentDistance;
    private float initialZ;
    

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        initialZ = startPos.z;
    }

    // Update is called once per frame
    void Update()
    {
        rotationAmountY = rotationAmountY - rotationSpeed;
        // Debug.Log(rotationAmountY.ToString());
        transform.rotation = Quaternion.Euler(0, rotationAmountY, 0);

        currentDistance = (transform.position - startPos).magnitude;
        if (currentDistance > maxDistance) {
            directionMultiplier = -directionMultiplier;
            startPos = transform.position;
        }
        // currentPositionZ = currentPositionZ + moveAmountZ;
        currentPositionZ += moveAmountZ * directionMultiplier;
        transform.position = new Vector3(transform.position.x, transform.position.y, initialZ + currentPositionZ);
    }

}
