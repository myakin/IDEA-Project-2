using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour {
    public static DataManager instance;

    private void Awake() {
        if (DataManager.instance==null) {
            DataManager.instance = this;
        } else {
            if (DataManager.instance!=this) {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }// singleton

    public int sessionId;
    public int playerLife;
    public int playerScore;
    public int playerHealth;


    public List<int> collectedKeys = new List<int>();

    public void Start() {
        string sessionIdDataPath = Application.persistentDataPath + "/initialData2.ideaprj1sav";
        if (File.Exists(sessionIdDataPath)) {
            int lastPlayedSessionId = 0;
            sessionId = LoadData(lastPlayedSessionId, sessionIdDataPath);
            LoadGame();
        } else {
            NewGame();
        }
    }

    public void IncreasePlayerScore() {
        playerScore++;
        UIManager.instance.UpdatePlayerScore();
    }

    public void ResetData() {
        playerScore = 0;
        playerLife = 3;
        playerHealth = 100;
        collectedKeys.Clear();
    }

    public void AddToCollectedKeys(int aKeyId) {
        collectedKeys.Add(aKeyId);
    }
    public void ClearCollectiblesData() {
        collectedKeys.Clear();
    }

    public void SetLife(int aValue) {
        playerLife = aValue;
        UIManager.instance.SetLife();
    }

    public void NewGame() {
        sessionId = GetToday();
        ResetData();
        SceneLoader.instance.LoadScene("Scene1");
    }



    public void SaveGame() {
        string sceneName = SceneLoader.instance.GetSceneName();
        PlayerData playerData = new PlayerData(playerLife, playerScore, playerHealth);
        CollectiblesData collectiblesData = new CollectiblesData(collectedKeys);
        List<MovingObjectSaveData> movingObjectsData = new List<MovingObjectSaveData>();
        ObjectMovement[] movingObjects = GameObject.FindObjectsOfType<ObjectMovement>();
        for (int i=0; i<movingObjects.Length; i++) {
            MovingObjectSaveData movingObjectSave = new MovingObjectSaveData(
                movingObjects[i].transform.position,
                movingObjects[i].moveRange,
                movingObjects[i].moveMultiplier,
                movingObjects[i].moveSpeed,
                movingObjects[i].startPos,
                movingObjects[i].moveDir,
                movingObjects[i].endPos
            );
            movingObjectsData.Add(movingObjectSave);
        }        

        SceneData dataToSave = new SceneData(
            playerData,
            collectiblesData,
            movingObjectsData
        );

        string filePath = Application.persistentDataPath + "/savedSceneData_"+ sessionId +"_"+sceneName+".ideaprj1sav"; //savedSceneData_123456_Scene1.ideaprj1sav    savedSceneData_123456_Scene2.ideaprj1sav    
        SaveData(dataToSave, filePath);

        SaveData(sceneName, Application.persistentDataPath + "/initialData.ideaprj1sav");
        SaveData(sessionId, Application.persistentDataPath + "/initialDataId.ideaprj1sav");
    }

    public void LoadGame(bool skipPlayer = false) {
        string initInfoFilePath = Application.persistentDataPath + "/initialData.ideaprj1sav";
        string lastPlayedSceneName = "";
        lastPlayedSceneName = LoadData(lastPlayedSceneName, initInfoFilePath);

        // TODO: is this needed?
        // string sessionIdDataPath = Application.persistentDataPath + "/initialData2.ideaprj1sav";
        // int lastPlayedSessionId = 0;
        // sessionId = LoadData(lastPlayedSessionId, sessionIdDataPath);

        string filePath = Application.persistentDataPath + "/savedSceneData_"+ sessionId +"_"+lastPlayedSceneName+".ideaprj1sav"; 
        SceneData loadedData = new SceneData();
        loadedData = LoadData(loadedData, filePath);

        // set player data
        if (skipPlayer) {
            playerLife = loadedData.playerData.life;
            playerScore = loadedData.playerData.score;
            playerHealth = loadedData.playerData.health;
        }

        // set collectibels data
        collectedKeys = loadedData.collectiblesData.collectedKeyIds;



        // load level and set moving object positions
        SceneLoader.instance.LoadScene(lastPlayedSceneName);

        // set moving object positions
        ObjectMovement[] movingObjects = GameObject.FindObjectsOfType<ObjectMovement>();
        for (int i=0; i<movingObjects.Length; i++) {
            movingObjects[i].SetEnemyData(
                loadedData.movingObjectSaveData[i].currentPosition,
                loadedData.movingObjectSaveData[i].range,
                loadedData.movingObjectSaveData[i].multiplier,
                loadedData.movingObjectSaveData[i].speed,
                loadedData.movingObjectSaveData[i].sPos,
                loadedData.movingObjectSaveData[i].dir,
                loadedData.movingObjectSaveData[i].ePos
            );
        }

    }

    public void LoadSceneData(string aSceneName) {
        string sceneDataPath = Application.persistentDataPath + "/savedSceneData_"+ sessionId +"_"+ aSceneName + ".ideaprj1sav";
        if (File.Exists(sceneDataPath)) {
            LoadGame(true);
        } else {
            SceneLoader.instance.LoadScene(aSceneName);
        }
    }


    



    // Utilities++++++++++++++++++++++++++++++++++++++++++++
    private void SaveData<T>(T aSaveDataClass, string aPath) {
        FileStream stream = new FileStream(aPath, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        string jsonString = JsonUtility.ToJson(aSaveDataClass);
        bf.Serialize(stream, jsonString);
        stream.Close();
    }
    private T LoadData<T>(T aDataClassToBeAssigned, string aPath) {
        if (File.Exists(aPath)) {
            FileStream stream = new FileStream(aPath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            string jsonString = (string)bf.Deserialize(stream);
            aDataClassToBeAssigned = JsonUtility.FromJson<T>(jsonString);
            stream.Close();
        }
        return aDataClassToBeAssigned;
    }

    public bool DoesSceneSaveExist(string aSceneName) {
        string sceneDataPath = Application.persistentDataPath + "/savedSceneData_"+ sessionId +"_"+ aSceneName + ".ideaprj1sav";
        if (File.Exists(sceneDataPath)) {
            return true;
        }
        return false;
    }

    private int GetToday() {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        return cur_time;
    } 
    // Utilities---------------------------------------------

}
