using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] private string startGameScene;
    [SerializeField] private string moreInfo_Link;
    [SerializeField] private GameObject defaultMenuPanel;
    private bool creditsPlaying = false;

    private void Start() {
        OnSceneLoaded();
    }

    private void Update() {
        if (!creditsPlaying) { return; }
        if (UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame) {
            this.GetComponentInChildren<Animator>().Play("New State");
            OnCreditsDone();
        }
    }

    public void OnSceneLoaded() {
        if (!GameManager.Instance.startMainMenuWithCredits) {
            defaultMenuPanel.SetActive(true);
            return;
        }
        PlayCredits();
    }

    public void OnStartButtonDown() {
        SceneManager.LoadScene(startGameScene);
    }

    public void OnQuitButtonDown() {
        Application.Quit();
    }

    public void OnMoreInfoButtonDown() {
        Application.OpenURL(moreInfo_Link);
    }

    public void PlayCredits() {
        defaultMenuPanel.SetActive(false);
        this.GetComponentInChildren<Animator>().Play("Credits_Scroll");
        creditsPlaying = true;
    }

    public void OnCreditsDone() {
        defaultMenuPanel.SetActive(true);
        GameManager.Instance.startMainMenuWithCredits = false;
        creditsPlaying = false;
    }
}
