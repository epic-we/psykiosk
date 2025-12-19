using UnityEngine;

public class DialogueChoiceButton : MonoBehaviour {
    private int choiceID;

    public void SetChoiceID(int newID) {
        choiceID = newID;
    }
    
    public void OnChoiceButtonDown() {
        DialogueManager.Instance.ContinueWithChoice(choiceID);
    }
}
