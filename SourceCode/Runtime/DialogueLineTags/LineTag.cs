using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LineTag", menuName = "Custom Systems/Dialogue/LineTag")]
public class LineTag : ScriptableObject {
    public string testString;

    public List<ActorPositionChange> actorPositions = new List<ActorPositionChange>();
    public List<ActorAnimationChange> actorAnimations = new List<ActorAnimationChange>();
    public List<Bubble> bubbles = new List<Bubble>();
    public List<ItemSpawn> itemSpawns = new List<ItemSpawn>();
    public List<AudioClip> audioClips = new List<AudioClip>();

    public bool isEnding;
    public bool isExplosionEnding;
    public bool isGunEnding;
    [TextArea(10, 15)]
    public string endingText;
}

[System.Serializable]
public class ActorPositionChange {
    public Actor actorToMove;
    public Vector3 position;
    public bool placeActorBehindCounter;
}

[System.Serializable]
public class ActorAnimationChange {
    public Actor actorToAnimate;
    public string animationStateName;
}

[System.Serializable]
public class Bubble {
    public Actor actorSpeaking;
    public string bubbleLine;
}

[System.Serializable]
public class ItemSpawn {
    public string itemType;
    public Vector3 position;
}
