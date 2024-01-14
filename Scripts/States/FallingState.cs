using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : IState
{
    Vector3 point_dir;
    float fall_speed = 2f;
    float fall_time;
    float fall_timer = 0f;
    Rigidbody2D rb;
    Action on_done;
    public FallingState(Rigidbody2D rb, float fl_spd, Vector3 pt_dir, float fl_time, Action change_to_recovery) {
        this.rb = rb;
        fall_speed = fl_spd;
        point_dir = pt_dir;
        fall_time = fl_time;
        on_done = change_to_recovery;
    }
    void IState.enter()
    {
        fall_timer = 0f;
        rb.velocity = point_dir * fall_speed;
    }

    void IState.execute()
    {
        rb.velocity = point_dir * fall_speed;

        fall_timer += Time.deltaTime;
        if (fall_timer >= fall_time) {
            fall_timer = 0f;
            on_done();
        }
    }

    void IState.exit()
    {
        rb.velocity = Vector2.zero;
    }
}