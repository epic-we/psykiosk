using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour {
    [SerializeField] private GameObject escapeMenu;

    private void Update() {
        if (UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame){
            ToggleEscapeMenu();
        }
    }

    private void ToggleEscapeMenu() {
        if (escapeMenu.activeInHierarchy) {
            CloseEscapeMenu();
            return;
        } else {
            OpenEscapeMenu();
        }
    }

    public void OpenEscapeMenu() {
        //Toggle pause screen
        escapeMenu.SetActive(true);
        GameManager.Instance.PauseGame(true);
    }

    public void CloseEscapeMenu() {
        //Toggle pause screen
        escapeMenu.SetActive(false);
        GameManager.Instance.PauseGame(false);
    }

    public void OnRestartButton() {
        DialogueManager.Instance.SkipToEndOfDialogueLine();
        DialogueManager.Instance.EndStory();
        CloseEscapeMenu();
        GameManager.Instance.RestartGame();
    }

    public void OnMainMenuButtonDown(){
        GameManager.Instance.PauseGame(false);
        SceneManager.LoadScene("MainMenu");
    }
}
