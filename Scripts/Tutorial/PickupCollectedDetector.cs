using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollectedDetector : MonoBehaviour
{
    public int id;
    public GameObject pickup_prefab;
    void OnTriggerEnter2D(Collider2D col) {
        int children_count = transform.childCount;
        if (children_count == 0) {
            spawn_pickup();
        }
    }
    void spawn_pickup() {
        Instantiate(pickup_prefab, transform.position, Quaternion.identity);
    }
}
