using UnityEngine;

public class AudioManager : MonoBehaviour {
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [SerializeField] private AudioSource audioSource_Music;
    [SerializeField] private AudioSource audioSource_SFX;
    [SerializeField] private AudioClip[] SFX_typingClips;
    [SerializeField] private AudioClip SFX_gunClip;
    [SerializeField] private float gunVolume;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlaySFXOneShot(AudioClip clip, float volume) {
        audioSource_SFX.PlayOneShot(clip, volume);
    }

    public void ToggleTyping(bool toggle){
        if (toggle) {
            audioSource_SFX.PlayOneShot(FetchRandomAudioClip(SFX_typingClips));
        } else {
            audioSource_SFX.Stop();
            audioSource_SFX.clip = null;
        }
    }

    private AudioClip FetchRandomAudioClip(AudioClip[] audioClipArray) {
        int random = Random.Range(0, audioClipArray.Length);
        return audioClipArray[random];
    }

    public void PlayGunSound() {
        PlaySFXOneShot(SFX_gunClip, gunVolume);
    }

}
