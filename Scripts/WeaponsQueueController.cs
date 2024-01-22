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
                break;
            }
        }
        return next_weapon_prefab;
    }

    public GameObject peek_next_weapon() {
        GameObject next_weapon_prefab = null;
        for (int i = 0; i < weapons_queue.Length; i++) {
            GameObject current = weapons_queue[i];
            if (current != null) {
                next_weapon_prefab = current;
                break;
            }
        }
        return next_weapon_prefab;
    }

    public bool add_weapon(GameObject new_weapon_prefab) {
        for (int i = 0; i < weapons_queue.Length; i++) {
            if (weapons_queue[i] == null) {
                weapons_queue[i] = new_weapon_prefab;
                return true;
            }
        }
        return false;
    }

    public void copy_queue(WeaponsQueueController source_script) {
        GameObject[] temp = source_script.get_queue();
        // It's because weapons_queue has a length of 0
        for (int i = 0; i < temp.Length; i++) {
            weapons_queue[i] = temp[i];
        }
    }
    public GameObject[] get_queue() {
        return weapons_queue;
    }
    public void clear() {
        for (int i = 0; i < weapons_queue.Length; i++) {
            weapons_queue[i] = null;
        }
    }
}
