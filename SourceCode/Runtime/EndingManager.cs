using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndingManager : MonoBehaviour {
    [SerializeField] private GameObject endingObject;
    [SerializeField] private TMP_Text _endingText;

    void Start() {
        ToggleEndingObject(false);
    }

    public void ShowEnding(LineTag endingTag) {
        FindAnyObjectByType<MouseFollowCamera>().ToggleCameraMovement(false);
        FindAnyObjectByType<DialogueUI>().ToggleDialogueUI(false);
        ToggleEndingObject(true);
        _endingText.text = endingTag.endingText;
    }

    public void ToggleEndingObject(bool toggle) {
        endingObject.SetActive(toggle);
    }

    public void OnTryAgainButton() {
        GameManager.Instance.RestartGame();
        FindFirstObjectByType<SceneTransition>().PlayFadeInShort();
    }

    public void OnQuitToCreditsButton() {
        Debug.Log("Load Credits");
    }


    public void OnMainMenuButtonDown(){
        GameManager.Instance.startMainMenuWithCredits = true;
        SceneManager.LoadScene("MainMenu");
    }

}
