using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;

public class GrenadeController : WeaponController
{
    [SerializeField] private float num_shrapnel;
    float current_time = 0f;
    float max_time = 3f;
    protected override void Start()
    {
        base.Start();
        fire_speed = 5f;
    }
    void Update()
    {
        if (set_to_destroy) {
            return;
        }
        current_time += Time.deltaTime;
        if (current_time >= max_time) {
            fire_next();
            StartCoroutine(delayed_destroy());
        }
    }
    protected override void fire_next() {
        GameObject next_weapon_prefab = weapon_queue_script.pop_next_weapon();
        if (next_weapon_prefab == null) {
            // Debug.Log("nothing to shoot");
            return;
        }
        for (int i = 0; i < num_shrapnel; i++) {
            GameObject weapon_object = Instantiate(next_weapon_prefab, transform.position, Quaternion.identity);
            Rigidbody2D weapon_body = weapon_object.GetComponent<Rigidbody2D>();
            WeaponController weapon_script = weapon_object.GetComponent<WeaponController>();
            Transform weapon_transform = weapon_object.transform;
            weapon_transform.localScale = transform.localScale;
            Quaternion rotate_amount = Quaternion.Euler(0, 0, -(360 / num_shrapnel) * i);
            Vector3 new_point_dir = rotate_amount * point_dir;
            Vector2 adjusted_velocity = rotate_amount * rb.velocity;
            weapon_body.velocity = adjusted_velocity + (Vector2)(new_point_dir * fire_speed);
            // The commented out line leads to unintended and kind of unintuitive behavior
            // but it's interesting behavior that I think adds a more interesting choice in using a gun
            // weapon_script.initialize(2, weapon_queue_script, point_dir);
            weapon_script.initialize(2, weapon_queue_script, new_point_dir, firer);
        }
        weapon_queue_script.clear();
    }
}
