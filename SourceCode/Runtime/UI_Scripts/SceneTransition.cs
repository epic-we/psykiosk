using UnityEngine;

public class SceneTransition : MonoBehaviour {
    [SerializeField] private AudioClip explosionAudio;
    [SerializeField] private float explosionVolume;

    public void OnFadeInDone() {
        GameManager.Instance.StartGame();
    }

    public void PlayFadeInShort() {
        this.GetComponent<Animator>().Play("Animation_FadeFromBlack_Short");
    }

    public void PlayExplosion() {
        this.GetComponent<Animator>().Play("Animation_Explosion");
    }

    public void PlayExplosionAudio() {
        AudioManager.Instance.PlaySFXOneShot(explosionAudio, explosionVolume);
    }

    public void OnExplosionFinished() {
        FindFirstObjectByType<TagProcessor>().ProcessEndingOnly();
    }
}
