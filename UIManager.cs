using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image[] weapon_slots;
    [SerializeField] Sprite[] default_images;
    public void update_queue(WeaponsQueueController src_script) {
        GameObject[] queue = src_script.get_queue();
        for (int i = 0; i < queue.Length; i++) {
            if (queue[i] != null) {
                weapon_slots[i].sprite = queue[i].GetComponent<SpriteRenderer>().sprite;
            } else {
                weapon_slots[i].sprite = default_images[i];
            }
        }
    }
}