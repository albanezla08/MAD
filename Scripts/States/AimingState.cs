using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingState : IState
{
    Transform transform, player_transform;
    Rigidbody2D rb;
    public AimingState(Rigidbody2D rb, Transform own_tr, Transform player_tr) {
        this.rb = rb;
        transform = own_tr;
        player_transform = player_tr;
    }
    void IState.enter()
    {
        // Vector2 dir = player_transform.position - transform.position;
        // dir = dir.normalized;
        // rb.velocity = dir * chase_speed;
    }

    void IState.execute()
    {
        Vector2 dir = player_transform.position - transform.position;
        // dir = dir.normalized;
        // dir.z = 0;
        Debug.Log("aiming");

        Vector3 m_EulerAngleVelocity = new Vector3(0, 100, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation + 50f * Time.fixedDeltaTime);

        // Quaternion target_rotation = Quaternion.LookRotation(dir, Vector3.forward);
        // float str = Mathf.Min (0.5f * Time.deltaTime, 1);
        // transform.rotation = (Quaternion.Lerp(transform.rotation, target_rotation, Time.deltaTime * str));

        // rb.velocity = dir * chase_speed;
        // if (Vector2.Distance(transform.position, player_transform.position) < player_in_range_distance) {
        //     on_player_in_range();
        // }
    }

    void IState.exit()
    {

    }
}