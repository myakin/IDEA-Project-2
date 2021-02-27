using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYPlayerScoreManager : MonoBehaviour {
    public int score;

    public void Increase(int aValue) {
        score+=aValue;
        DataManager.instance.SetPlayerScore(score);
    }
}
