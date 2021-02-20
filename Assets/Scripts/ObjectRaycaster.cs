using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRaycaster : MonoBehaviour {
    public Vector3 raycastAxis;
    public float raycastDistance;
    public LayerMask raycastLayer;

    public delegate void OnHitAction();

    public void PerformRaycast(OnHitAction onHit) {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastAxis, out hit, raycastDistance, raycastLayer, QueryTriggerInteraction.Ignore)) {
            if (onHit!=null) {
                onHit();
            }
        }
    }


}
