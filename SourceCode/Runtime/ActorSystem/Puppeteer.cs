using UnityEngine;

public class Puppeteer : MonoBehaviour {
    public Actor actor;
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private bool behindCounterOnStart;
    [SerializeField] private string startingAnimation;
    private SpriteRenderer characterSprite;
    private Animator animator;
    public Transform bubbleAnchor;

    private void Start() {
        startingPosition = this.transform.position;
        characterSprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        ResetActor();
    }

    public void MoveActorPosition(Vector3 position, bool placeBehindCounter) {
        if (characterSprite == null) {
            characterSprite = GetComponentInChildren<SpriteRenderer>();
        }

        this.gameObject.transform.position = position;
        if (placeBehindCounter) {
            characterSprite.sortingOrder = 3;
        } else {
            characterSprite.sortingOrder = 10;
        }
    }

    public void SetAnimation(string animationClipName) {
        if (animator == null) {
            animator = GetComponentInChildren<Animator>();
        }

        animator.Play(animationClipName);
    }

    public void ResetActor() {
        MoveActorPosition(startingPosition, behindCounterOnStart);
        SetAnimation(startingAnimation);
    }
}
