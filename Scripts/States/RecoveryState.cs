using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState : IState
{
    float recovery_time;
    float recovery_timer = 0f;
    Action on_done;
    Rigidbody2D rb;
    SpriteRenderer stars_renderer;
    public RecoveryState(Rigidbody2D rb, float rec_time, Action change_to_wander, SpriteRenderer stars_renderer) {
        recovery_time = rec_time;
        on_done = change_to_wander;
        this.rb = rb;
        this.stars_renderer = stars_renderer;
    }
    void IState.enter()
    {
        recovery_timer = 0f;
        rb.velocity = Vector2.zero;
        stars_renderer.enabled = true;
    }

    void IState.execute()
    {
        recovery_timer += Time.deltaTime;
        if (recovery_timer >= recovery_time) {
            recovery_timer = 0f;
            on_done();
        }
    }

    void IState.exit()
    {
        stars_renderer.enabled = false;
    }
}