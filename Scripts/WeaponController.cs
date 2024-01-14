using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected int next_weapons_count;
    protected WeaponsQueueController weapon_queue_script;
    protected Vector3 point_dir;
    protected float fire_speed = 1f;
    public void initialize(int count, WeaponsQueueController source_queue, Vector3 direction) {
        next_weapons_count = count;
        weapon_queue_script = gameObject.GetComponent<WeaponsQueueController>();
        weapon_queue_script.copy_queue(source_queue);
        GameObject[] queue = weapon_queue_script.get_queue();
        for (int i = 0; i < queue.Length; i++) {
            Debug.Log(queue[i]);
        }
        point_dir = direction;
    }

    protected virtual void fire_next() {

    }
}
