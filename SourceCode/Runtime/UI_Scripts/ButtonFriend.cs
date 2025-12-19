using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonFriend : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [Header("======== Text Visuals ========")]
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color selectedTextColour;
    [SerializeField] private Color deselectedTextColour;
    [SerializeField] private Color unavailableTextColour;

    [Header("======== Audio ========")]
    [SerializeField] private bool playAudio;
    [SerializeField] private AudioClip onSelectAudio;
    [SerializeField] private float onSelectVolume;
    [SerializeField] private AudioClip onDeselectAudio;
    [SerializeField] private float onDeselectVolume;
    [SerializeField] private AudioClip onButtonDownAudio;
    [SerializeField] private float onButtonDownVolume;
    [SerializeField] private AudioClip onUnavailableAudio;
    [SerializeField] private float onUnavailableVolume;

    private void Selected() {
        if (text != null) {
            text.color = selectedTextColour;
        }

        if (playAudio && onSelectAudio != null) {
            AudioManager.Instance.PlaySFXOneShot(onSelectAudio, onSelectVolume);
        }
    }

    public void Deselected() {
        if (text != null) {
            text.color = deselectedTextColour;
        }

        if (playAudio && onDeselectAudio != null){
            AudioManager.Instance.PlaySFXOneShot(onDeselectAudio, onDeselectVolume);
        }
    }

    public void Unavailable() {
        if (text != null) {
            text.color = unavailableTextColour;
        }
    }

    private void OnClickEffects() {
        if (playAudio && onButtonDownAudio != null) {
            AudioManager.Instance.PlaySFXOneShot(onButtonDownAudio, onButtonDownVolume);
        }
        if (text != null) {
            text.color = deselectedTextColour;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
        if (!this.GetComponent<Button>().IsInteractable()) { return; }
        Selected();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!this.GetComponent<Button>().IsInteractable()) { return; }
        Deselected();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        if (!this.GetComponent<Button>().IsInteractable()) { return; }
        OnClickEffects();
    }
}
