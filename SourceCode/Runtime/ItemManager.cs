using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private GameObject turtlePrefab;
    [SerializeField] private List<GameObject> currentActiveItems = new List<GameObject>();

    public void SpawnItem(ItemSpawn itemSpawn) {
        switch (itemSpawn.itemType) {
            case "Gun":
                currentActiveItems.Add(Instantiate(gunPrefab, itemSpawn.position, Quaternion.identity, this.transform));
                break;

            case "Turtle":
                currentActiveItems.Add(Instantiate(turtlePrefab, itemSpawn.position, Quaternion.identity, this.transform));
                break;

            default:
                Debug.Log("Invalid Item Type!!");
                break;
        }
    }

    public void CheckAndDestroyItems() {
        if (currentActiveItems.Count == 0) { return; }

        foreach (GameObject activeItem in currentActiveItems){
            Destroy(activeItem);
        }

        currentActiveItems.Clear();
    }
}
