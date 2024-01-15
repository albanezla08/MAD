using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image[] weapon_slots;
    [SerializeField] Sprite[] default_images;
    [SerializeField] Image[] hearts;
    [SerializeField] Text score;
    [SerializeField] Text need_weapon;
    [SerializeField] float show_need_weapon_duration;
    bool is_need_weapon_showing = false;
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

    public void update_hearts(int remaining) {
        for (int i = 0; i < hearts.Length; i++) {
            hearts[i].enabled = i < remaining;
        }
    }

    public void update_score(int score) {
        this.score.text = score.ToString();
    }

    public void show_need_weapon() {
        if (is_need_weapon_showing) {
            return;
        }
        is_need_weapon_showing = true;
        need_weapon.enabled = true;
        StartCoroutine(hide_after(show_need_weapon_duration));
    }
    IEnumerator hide_after(float seconds) {
        yield return new WaitForSeconds(seconds);
        hide_need_weapon();
    }
    private void hide_need_weapon() {
        need_weapon.enabled = false;
        is_need_weapon_showing = false;
    }
}
