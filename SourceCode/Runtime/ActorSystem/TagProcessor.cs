using System.Collections.Generic;
using UnityEngine;

public class TagProcessor : MonoBehaviour {
    [SerializeField] private List<LineTag> allLineTags = new List<LineTag>();
    [SerializeField] private List<LineTag> allLineTags_endings = new List<LineTag>();
    [SerializeField] private List<Puppeteer> puppeteers = new List<Puppeteer>();
    private BubbleManager bubbleManager;
    private ItemManager itemManager;
    private string temporaryHeldEnding;

    private void Start() {
        FindAllActors();
        bubbleManager = FindFirstObjectByType<BubbleManager>();
        itemManager = FindFirstObjectByType<ItemManager>();
    }

    private void FindAllActors(){
        foreach (Puppeteer puppeteer in Resources.FindObjectsOfTypeAll(typeof(Puppeteer))) {
            puppeteers.Add(puppeteer);
        }
    }

    public void ProcessTag(string tagName) {
        itemManager.CheckAndDestroyItems();

        //Debug.Log(tagName);
        if (FetchRelevantEndingTag(tagName)) {
            Debug.Log("We found an ending");
            LineTag currentEndingTag = FetchRelevantEndingTag(tagName);
            if (currentEndingTag.isExplosionEnding) {
                temporaryHeldEnding = tagName;
                FindFirstObjectByType<SceneTransition>().PlayExplosion();
                Debug.Log("We've gone to scene transition!");
                return;
            }
            if (currentEndingTag.isGunEnding){
                AudioManager.Instance.PlayGunSound();
            }
            FindAnyObjectByType<EndingManager>().ShowEnding(FetchRelevantEndingTag(tagName));
        }

        if (FetchRelevantLineTag(tagName) == null) {
            return;
        }
        LineTag currentTag = FetchRelevantLineTag(tagName);
        //Debug.Log(currentTag.testString);
        foreach (ActorPositionChange actorPosition in currentTag.actorPositions) {
            HandleActorPositions(actorPosition);
        }
        foreach (ActorAnimationChange actorAnimation in currentTag.actorAnimations){
            HandleActorAnimation(actorAnimation);
        }
        foreach (AudioClip audioClip in currentTag.audioClips) {
            AudioManager.Instance.PlaySFXOneShot(audioClip, 1f);
        }
        foreach (Bubble bubble in currentTag.bubbles) {
            HandleBubbles(bubble);
        }
        foreach (ItemSpawn itemSpawn in currentTag.itemSpawns) {
            HandleItems(itemSpawn);
        }
    }

    public void ProcessEndingOnly() {
        if (temporaryHeldEnding == "") { return; }
        FindAnyObjectByType<EndingManager>().ShowEnding(FetchRelevantEndingTag(temporaryHeldEnding));
        temporaryHeldEnding = "";
    }
    

    private void HandleActorPositions(ActorPositionChange actorPosition) {
        Debug.Log("We're moving " + actorPosition.actorToMove + " to " + actorPosition.position);
        FetchRelevantPuppeteer(actorPosition.actorToMove.actorName).MoveActorPosition(actorPosition.position, actorPosition.placeActorBehindCounter);

    }

    private void HandleActorAnimation(ActorAnimationChange actorAnimation) {
        FetchRelevantPuppeteer(actorAnimation.actorToAnimate.actorName).SetAnimation(actorAnimation.animationStateName);

    }

    private void HandleBubbles(Bubble bubble) {
        bubbleManager.SpawnBubble(bubble.bubbleLine, FetchRelevantPuppeteer(bubble.actorSpeaking.actorName));
    }

    private void HandleItems(ItemSpawn itemSpawn) {
        itemManager.SpawnItem(itemSpawn);
    }

    private LineTag FetchRelevantLineTag(string tagName) {
        for (int i = 0; i < allLineTags.Count; i++) {
            if (allLineTags[i].name == tagName) {
                return allLineTags[i];
            }
        }
        Debug.Log("ERROR: Line Tag doesn't exist!!");
        return null;
    }

    private LineTag FetchRelevantEndingTag(string tagName) {
        for (int i = 0; i < allLineTags_endings.Count; i++) {
            if (allLineTags_endings[i].name == tagName) {
                return allLineTags_endings[i];
            }
        }
        return null;
    }

    private Puppeteer FetchRelevantPuppeteer(string actorName) {
        for (int i = 0; i < puppeteers.Count; i++) {
            if (puppeteers[i].actor.actorName == actorName) {
                return puppeteers[i];
            }
        }
        Debug.Log("ERROR: Can't find Puppeteers!!");
        return null;
    }
}
