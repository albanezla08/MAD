using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : IState
{
    Vector2 wander_delay_timer_range = new Vector2(1.6f, 3f);
    float wander_delay;
    float wander_delay_timer = 0f;
    Vector3 dir_to_choose;
    float move_speed = 2f;
    Rigidbody2D rb;
    public WanderState(Rigidbody2D rb, float mv_spd) {
        this.rb = rb;
        move_speed = mv_spd;
    }
    void IState.enter()
    {
        wander_delay = Random.Range(wander_delay_timer_range.x, wander_delay_timer_range.y);
        dir_to_choose = choose_dir();
    }

    void IState.execute()
    {
        wander_logic();

        rb.velocity = dir_to_choose * move_speed;
    }

    void IState.exit()
    {

    }

    void wander_logic() {
        if (wander_delay_timer >= wander_delay) {
            dir_to_choose = choose_dir();
            wander_delay_timer = 0;
        } else {
            wander_delay_timer += Time.deltaTime;
        }
    }

    Vector3 choose_dir() {
        int dir = Random.Range(1, 5);   // creates a number between 1 and 4
        //Debug.Log(dir);
        Vector3 wanted_vector;
        if (dir == 1) {
            wanted_vector = Vector3.up;
        } else if (dir == 2) {
            wanted_vector = Vector3.right;
        } else if (dir == 3) {
            wanted_vector = Vector3.down;
        } else {
            wanted_vector = Vector3.left;
        }
        return wanted_vector;
    }
}