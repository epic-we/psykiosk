using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [Header("====== Game Setup ======")]
    [SerializeField] private float pauseBeforeGameStart = 0.5f;

    [Header("====== Story Setup ======")]
    [SerializeField] private bool playStoryAtStart;
    [SerializeField] private TextAsset _storyToPlayAtStart;

    [Header("====== Story Setup ======")]
    public bool startMainMenuWithCredits;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void StartGame() {
        StartCoroutine(PauseBeforeGameStart());
        
        IEnumerator PauseBeforeGameStart() {
            yield return new WaitForSeconds(pauseBeforeGameStart);
            StartDialogue();
        }
    }

    public void StartDialogue() {
        if (playStoryAtStart) {
            DialogueManager.Instance.PlayStoryAsset(_storyToPlayAtStart);
        }
    }

    public void RestartGame() {
        FindAnyObjectByType<EndingManager>().ToggleEndingObject(false);
        FindAnyObjectByType<MouseFollowCamera>().ToggleCameraMovement(true);
        ResetAllGameStats();
        StartGame();
    }

    public void ResetAllGameStats() {
        foreach (Puppeteer puppeteer in Resources.FindObjectsOfTypeAll(typeof(Puppeteer))) {
            puppeteer.ResetActor();
        }
    }

    public void PauseGame(bool toggle) {
        if (toggle) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }
}
