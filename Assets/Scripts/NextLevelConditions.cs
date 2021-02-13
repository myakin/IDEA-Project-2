using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelConditions : MonoBehaviour {
    

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // varsa level gecme sartlarini degerlendir


            // sartlar uygunsa yeni sahneyi yukle
            SceneLoader.instance.LoadNextLevel("Scene2");

        }
    }
}
