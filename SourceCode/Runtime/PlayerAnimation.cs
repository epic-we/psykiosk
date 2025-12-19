using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    [SerializeField] private GameObject blueThing;
    
    public void OnShowBlue() {
        blueThing.SetActive(true);
        blueThing.transform.position = this.transform.position;
    }

    public void OnHideBlue() {
        if (blueThing.activeInHierarchy) {
            blueThing.SetActive(false);
        }
    }
}
