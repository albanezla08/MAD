using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class AimingState : IState
{
    Transform transform, player_transform;
    Rigidbody2D rb;
    float max_time;
    float timer;
    float shoot_speed;
    Action on_done;
    public AimingState(Rigidbody2D rb, Transform own_tr, Transform player_tr, float max_time, float shoot_speed, Action change_to_falling) {
        this.rb = rb;
        transform = own_tr;
        player_transform = player_tr;
        this.max_time = max_time;
        this.shoot_speed = shoot_speed;
        on_done = change_to_falling;
    }
    void IState.enter()
    {
        // Vector2 dir = player_transform.position - transform.position;
        // dir = dir.normalized;
        // rb.velocity = dir * chase_speed;
        rb.velocity = Vector2.zero;
    }

    void IState.execute()
    {
        Vector2 dir = player_transform.position - transform.position;
        // dir = dir.normalized;
        // dir.z = 0;
        // Debug.Log("aiming");

        // transform.Rotate(0, 0, 90f);

        // Vector3 m_EulerAngleVelocity = new Vector3(0, 100, 0);
        // Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        // rb.MoveRotation(rb.rotation + 50f * Time.fixedDeltaTime);

        // Quaternion target_rotation = Quaternion.LookRotation(dir, Vector3.forward);
        // float str = Mathf.Min (0.5f * Time.deltaTime, 1);
        // transform.rotation = (Quaternion.Lerp(transform.rotation, target_rotation, Time.deltaTime * str));

        // rb.velocity = dir * chase_speed;
        // if (Vector2.Distance(transform.position, player_transform.position) < player_in_range_distance) {
        //     on_player_in_range();
        // }

        timer += Time.deltaTime;
        if (timer >= max_time) {
            timer = 0f;
            rb.velocity = dir * shoot_speed;
            on_done();
        }

    }

    void IState.exit()
    {

    }
}