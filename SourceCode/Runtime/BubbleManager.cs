using UnityEngine;
using System.Collections;
using TMPro;

public class BubbleManager : MonoBehaviour {
    [SerializeField] private Transform bubbleParent;
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float bubblePreWarmTime;
    [SerializeField] private float bubbleTime;
    private GameObject currentBubble;
    private Transform currentAnchor;
    [SerializeField] private Puppeteer testPuppeteer;

    private void Start() {
    }

    void Update() {
        if (currentBubble != null) {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(currentAnchor.position);
            currentBubble.transform.position = screenPos;
        }
    }

    public void SpawnBubble(string bubbleLine, Puppeteer pupeteer) {
        StartCoroutine(InstantiateBubble(bubbleLine, pupeteer));
    }

    private IEnumerator InstantiateBubble(string bubbleLine, Puppeteer pupeteer) {
        yield return new WaitForSeconds(bubblePreWarmTime);
        currentAnchor = pupeteer.bubbleAnchor;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(currentAnchor.position);
        currentBubble = Instantiate(bubblePrefab, screenPos, Quaternion.identity, bubbleParent);
        currentBubble.GetComponentInChildren<TMP_Text>().text = bubbleLine;
        StartCoroutine(DestroyBubble());
    }

    private IEnumerator DestroyBubble() {
        yield return new WaitForSeconds(bubbleTime);
        Destroy(currentBubble);
    }

    public void DestroyBubbleInstantly() {
        StopAllCoroutines();
        Destroy(currentBubble);
    }
}
