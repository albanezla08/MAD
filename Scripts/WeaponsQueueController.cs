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

    public void cycle_queue(bool is_dir_up) {
        int queue_length = weapons_queue.Length;
        if (is_dir_up) {
            GameObject first_weapon = weapons_queue[0];
            for (int i = 1; i < queue_length; i++) {
                weapons_queue[i-1] = weapons_queue[i];
            }
            weapons_queue[queue_length - 1] = first_weapon;
        } else {
            GameObject last_weapon = weapons_queue[queue_length - 1];
            for (int i = queue_length - 2; i > -1; i--) {
                weapons_queue[i+1] = weapons_queue[i];
            }
            weapons_queue[0] = last_weapon;
        }
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
