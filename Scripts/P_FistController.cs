using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;

public class P_FistController : WeaponController
{
    float current_time = 0f;
    float max_time = 3f;
    protected override void Start()
    {
        base.Start();
        fire_speed = 5f;
    }
    void Update()
    {
        current_time += Time.deltaTime;
        if (current_time >= max_time) {
            fire_next();
            Destroy(gameObject);
        }
    }
    protected override void fire_next() {
        GameObject next_weapon_prefab = weapon_queue_script.pop_next_weapon();
        if (next_weapon_prefab == null) {
            Debug.Log("nothing to shoot");
            return;
        }
        GameObject weapon_object = Instantiate(next_weapon_prefab, transform.position, Quaternion.identity);
        Rigidbody2D weapon_body = weapon_object.GetComponent<Rigidbody2D>();
        WeaponController weapon_script = weapon_object.GetComponent<WeaponController>();
        Transform weapon_transform = weapon_object.transform;
        weapon_transform.localScale = transform.localScale + new Vector3(1, 1, 0) * 0.5f;
        weapon_body.velocity = rb.velocity + (Vector2)(point_dir * fire_speed);
        weapon_script.initialize(2, weapon_queue_script, point_dir, firer);
    }
}
