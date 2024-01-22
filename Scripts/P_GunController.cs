using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GunController : WeaponController
{
    float current_time = 0f;
    float initial_velocity_magnitude;
    protected override void Start()
    {
        base.Start();
        transform.right = point_dir.normalized;
        // fire_speed = 10f;
        // audio_manager_script.play_clip("Punch Throw");
    }
    void Update()
    {
        if (set_to_destroy) {
            return;
        }
        if (current_time >= duration) {
            fire_next();
            StartCoroutine(delayed_destroy());
        } else {
            current_time += Time.deltaTime;
        }
    }

    protected override void fire_next()
    {
        audio_manager_script.play_clip("Gun Shoot");
        GameObject next_weapon_prefab = weapon_queue_script.pop_next_weapon();
        if (next_weapon_prefab == null) {
            // Debug.Log("nothing to shoot");
            return;
        }
        initial_velocity_magnitude = rb.velocity.magnitude;
        StartCoroutine(aim_and_fire(() => create_weapon(next_weapon_prefab)));
    }

    void create_weapon (GameObject weapon_prefab) {
        GameObject weapon_object = Instantiate(weapon_prefab, transform.position, Quaternion.identity);
        Rigidbody2D weapon_body = weapon_object.GetComponent<Rigidbody2D>();
        WeaponController weapon_script = weapon_object.GetComponent<WeaponController>();
        Transform weapon_transform = weapon_object.transform;
        weapon_transform.localScale = transform.localScale;
        point_dir = transform.right;
        weapon_body.velocity = (Vector2)(point_dir * initial_velocity_magnitude) + (Vector2)(point_dir * fire_speed);
        weapon_script.initialize(2, weapon_queue_script, point_dir, firer);
    }

    IEnumerator aim_and_fire (Action callback) {
        Transform target = choose_target();
        if (target != null) {
            Vector2 target_dir = (target.position - transform.position).normalized;
            float timer = 0f;
            float max_time = 0.5f;
            while (timer < max_time) {
                timer += Time.deltaTime;
                transform.right = Vector2.Lerp(transform.right, target_dir, timer / max_time);
                yield return null;
            }
        }
        callback();
    }

    Transform choose_target () {
        Transform closest_target = null;
        float closest_dist_sqr = Mathf.Infinity;
        Vector3 current_pos = transform.position;
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 40f, LayerMask.GetMask("Enemy"));
        foreach (Collider2D target in targets) {
            Transform target_transform = target.transform;
            float targ_dist_sqr = (target_transform.position - current_pos).sqrMagnitude;
            if (targ_dist_sqr < closest_dist_sqr) {
                closest_dist_sqr = targ_dist_sqr;
                closest_target = target_transform;
            }
        }
        return closest_target;
    }
}
