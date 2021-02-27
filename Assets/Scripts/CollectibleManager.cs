using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour {

    public int id;
    public MYUnityEvent OnEnter;
    

    public void Start() {
        if (DataManager.instance.collectedKeys.Contains(id)) {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Player") {
            if (OnEnter!=null) {
                OnEnter.Invoke();
            }
            Destroy(gameObject);
        }
    }
}
