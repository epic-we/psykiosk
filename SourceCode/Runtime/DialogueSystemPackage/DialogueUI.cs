using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueUI : MonoBehaviour {
    [Header("====== UI References ======")]
    [SerializeField] private GameObject dialogueObject;
    [SerializeField] private TMP_Text _speechText;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Transform _choiceButtonParent;
    [SerializeField] private GameObject _choiceButtonPrefab;


    [Header("====== Setup ======")]
    [SerializeField] private float _textSpeed = .1f;

    private List<GameObject> _activeChoiceButtons;
    private bool typingInProgress;

    private void Awake() {
        _activeChoiceButtons = new List<GameObject>();
    }

    void Start() {
        _speechText.text = "";
        ToggleDialogueUI(false);
    }

    public void ToggleDialogueUI(bool toggle) {
        dialogueObject.SetActive(toggle);
    }

    public void PresentDialogueLine(string dialogueLine) {
        if (!dialogueObject.activeInHierarchy) {
            ToggleDialogueUI(true);
        }
        StopAllCoroutines();
        StartCoroutine(TypeOutDialogueLine(dialogueLine));
    }

    private IEnumerator TypeOutDialogueLine(string dialogueLine) {
        typingInProgress = true;
        AudioManager.Instance.ToggleTyping(true);
        _speechText.text = "";
        var text = dialogueLine;
        var waitForNextCharacter = new WaitForSeconds(_textSpeed);
        for (int i = 0; i < text.Length; i++) {
            _speechText.text += text[i];
            yield return waitForNextCharacter;
        }
        typingInProgress = false;
        AudioManager.Instance.ToggleTyping(false);
        DialogueManager.Instance.OnDialogueLineFinished();
    }

    public bool CheckIfTypingLineDone() {
        if (typingInProgress) { return true; }
        return false;
    }

    public void SkipToEndOfDialogueLine(string dialogueLine){
        StopAllCoroutines();
        _speechText.text = "";
        _speechText.text = dialogueLine;
        DoneWithLine();
    }

    private void DoneWithLine() {
        AudioManager.Instance.ToggleTyping(false);
        DialogueManager.Instance.OnDialogueLineFinished();
    }

    public void PresentChoices(List<Choice> currentChoices){
        int i = 0;
        foreach (var choice in currentChoices) {
            var button = Instantiate(_choiceButtonPrefab, _choiceButtonParent);
            _activeChoiceButtons.Add(button);

            var text = button.GetComponentInChildren<TMP_Text>();
            text.text = choice.text;
            button.GetComponent<DialogueChoiceButton>().SetChoiceID(i);

            i++;
            button.SetActive(true);
        }
    }

    public void ToggleContinueButton(bool interactableState){
        _continueButton.interactable = interactableState;
    }

    public void DestroyChoiceButtons() {
        foreach (var item in _activeChoiceButtons) {
            Destroy(item);
        }
        _activeChoiceButtons.Clear();
    }

    public void ContinueButton() {
        DialogueManager.Instance.ContinueButton();
    }
}
