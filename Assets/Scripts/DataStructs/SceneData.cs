using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData {
    public PlayerData playerData;
    public CollectiblesData collectiblesData;
    public List<MovingObjectSaveData> movingObjectSaveData;

    public SceneData() {}

    public SceneData(PlayerData aPlayerData, CollectiblesData aCollectiblesData, List<MovingObjectSaveData> aMovingObjectDataList) {
        this.playerData = aPlayerData;
        this.collectiblesData = aCollectiblesData;
        movingObjectSaveData = aMovingObjectDataList;
    }
}
