using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsQueueController : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons_queue;
    public GameObject pop_next_weapon() {
        GameObject next_weapon_prefab = null;
        for (int i = 0; i < weapons_queue.Length; i++) {
            GameObject current = weapons_queue[i];
            if (current != null) {
                next_weapon_prefab = current;
                weapons_queue[i] = null;
            }
        }
        return next_weapon_prefab;
    }
}
