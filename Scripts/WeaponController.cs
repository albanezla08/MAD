using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected int next_weapons_count;
    protected WeaponsQueueController weapon_queue_script;
    protected Vector3 point_dir;
    protected float fire_speed = 1f;
    public void initialize(int count, WeaponsQueueController weapons_queue, Vector3 direction) {
        next_weapons_count = count;
        weapon_queue_script = weapons_queue;
        point_dir = direction;
    }

    protected virtual void fire_next() {

    }
}
