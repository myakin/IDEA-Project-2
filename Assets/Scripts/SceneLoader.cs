using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public static SceneLoader instance;

    private void Awake() {
        SceneLoader.instance = this;
    }


    public void LoadNextLevel(string sceneName) {
        DataManager.instance.LoadSceneData(sceneName);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }  

    public void Reload() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        // int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public string GetSceneName() {
        return SceneManager.GetActiveScene().name;
    }

}
