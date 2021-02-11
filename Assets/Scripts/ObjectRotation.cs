using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public  float rotationSpeed;
    public Vector3 rotationAxis;

    // Update is called once per frame
    void Update()
    {    
        transform.rotation *= Quaternion.Euler(rotationAxis * rotationSpeed);
    }
}
