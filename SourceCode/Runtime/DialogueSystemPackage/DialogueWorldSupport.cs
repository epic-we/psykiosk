using UnityEngine;

public class DialogueWorldSupport : MonoBehaviour {

    [Header("====== Object Management ======")]
    [SerializeField] GameObject[] _disableOnDialogueStart;
    [SerializeField] GameObject[] _enableOnDialogueEnd;

    public void OnDialogueStarted() {
        // vvvvv Disables a series of objects, when the story launches vvvvv
        foreach (var gameObject in _disableOnDialogueStart) gameObject.SetActive(false);
    }

    public void OnDialogueEnded() {
        foreach (var gameObject in _disableOnDialogueStart) gameObject.SetActive(true);
    }
}
