using UnityEngine;
using Ink.Runtime;
using System.Collections;

public class DialogueManager : MonoBehaviour {
    private static DialogueManager instance;
    public static DialogueManager Instance { get { return instance; } }

    private DialogueUI dialogueUI;

    [Header("====== Story in progress ======")]
    private Story _currentStory;
    private TextAsset _currentStoryAsset;
    private string _currentDialogueLine;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayStoryAsset(TextAsset storyAsset) {
        this.GetComponent<DialogueWorldSupport>().OnDialogueStarted();

        _currentStoryAsset = storyAsset;
        _currentStory = new Story(storyAsset.text);
        Continue();
    }

    public void Continue() {
        if (dialogueUI == null) {
            dialogueUI = FindFirstObjectByType<DialogueUI>();
        }
        FindAnyObjectByType<BubbleManager>().DestroyBubbleInstantly();
        dialogueUI.ToggleContinueButton(_currentStory.canContinue);
        if (!_currentStory.canContinue) {
            EndStory();
            return;
        }
        PresentDialogueLine();
        ProcessTag();
    }

    private void PresentDialogueLine(){
        _currentDialogueLine = _currentStory.Continue();
        dialogueUI.PresentDialogueLine(_currentDialogueLine);
    }

    public void ContinueButton() {
        if (dialogueUI.CheckIfTypingLineDone() && _currentStory.canContinue) {
            _currentDialogueLine = "";
            Continue();
        } else {
            SkipToEndOfDialogueLine();
        }
    }

    public void SkipToEndOfDialogueLine() {
        dialogueUI.SkipToEndOfDialogueLine(_currentDialogueLine);
    }

    public void OnDialogueLineFinished() {
        //Is called from UI, after dialogueLine is done being typed.
        if (_currentStory.currentChoices.Count > 0){
            PresentChoices();
        }
    }

    private void PresentChoices() {
        dialogueUI.ToggleContinueButton(_currentStory.canContinue);
        dialogueUI.PresentChoices(_currentStory.currentChoices);
    }

    public void ContinueWithChoice(int choiceID){
        _currentStory.ChooseChoiceIndex(choiceID);
        dialogueUI.DestroyChoiceButtons();
        Continue();
    }

    public void EndStory(){
        Debug.Log("Story has ended.");

        _currentStory = null;
        _currentStoryAsset = null;
        dialogueUI.DestroyChoiceButtons();
        dialogueUI.ToggleDialogueUI(false);
        this.GetComponent<DialogueWorldSupport>().OnDialogueEnded();
    }

    private void ProcessTag() {
        string newTag = "";
        if (_currentStory.currentTags.Count > 0){
            newTag = _currentStory.currentTags[0];
        }
        FindFirstObjectByType<TagProcessor>().ProcessTag(newTag);
    } 
}
