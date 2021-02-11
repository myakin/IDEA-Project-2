using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    private void Awake() {
        UIManager.instance = this;
    }


    // Variables & references
    public Text scoreText, lifeText;
    // public int numOfKeys;
    public Button newGameButton, loadGameButton, saveGameButton;
    public GameObject mainMenuPanel;

    private void Start() {
        // numOfKeys = PlayerPrefs.GetInt("PlayerScore");
        // scoreText.text = numOfKeys.ToString();

        newGameButton.onClick.AddListener(NewGame);
        loadGameButton.onClick.AddListener(StartLoadingSavedGame);
        saveGameButton.onClick.AddListener(StartSavingGame);
        UpdatePlayerScore();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleMainMenu();
        }
    }

    public void UpdatePlayerScore() {
        // numOfKeys++;
        scoreText.text = DataManager.instance.playerScore.ToString();
        // PlayerPrefs.SetInt("PlayerScore",numOfKeys);
        // DataManager.instance.playerScore = numOfKeys;
    }

    public void NewGame() {
        DataManager.instance.NewGame();
    }

    private void ToggleMainMenu() {
        if (mainMenuPanel.activeSelf) {
            mainMenuPanel.SetActive(false);
        } else {
            mainMenuPanel.SetActive(true);
        }

        // mainMenuPanel.SetActive( mainMenuPanel.activeSelf ? false : true );
    }

    private void StartSavingGame() {
        DataManager.instance.SaveGame();
    }
    private void StartLoadingSavedGame() {
        DataManager.instance.LoadGame();
    }

    public void SetLife() {
        lifeText.text = DataManager.instance.playerLife.ToString();;
    }



}
