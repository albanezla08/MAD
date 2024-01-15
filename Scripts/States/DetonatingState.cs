using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class DetonatingState : IState
{
    Rigidbody2D rb;
    float max_time;
    float timer;
    Action on_done;
    Animator grenade_animator;
    public DetonatingState(Rigidbody2D rb, float max_time, Action detonate, Animator anim) {
        this.rb = rb;
        this.max_time = max_time;
        on_done = detonate;
        grenade_animator = anim;
    }
    void IState.enter()
    {
        rb.velocity = Vector2.zero;
        grenade_animator.SetTrigger("Detonate");
    }

    void IState.execute()
    {
        timer += Time.deltaTime;
        if (timer >= max_time) {
            timer = 0f;
            on_done();
        }

    }

    void IState.exit()
    {

    }
}