using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public int life;
    public int score;
    public int health;
    

    public PlayerData() {}

    public PlayerData(int aLifeAmount, int aScoreAmount, int aHealthAmount) {
        this.life= aLifeAmount;
        this.score = aScoreAmount;
        this.health = aHealthAmount;
    }
}
