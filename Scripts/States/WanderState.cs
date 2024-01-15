using System;
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
    Action on_player_detect;
    Transform transform, player_transform;
    float player_detect_distance;
    SpriteRenderer sprite_renderer;
    float refresh_timer = 0f;
    float refresh_time;
    Action on_refresh;
    public WanderState(Rigidbody2D rb, float mv_spd, Action on_player_detect, Transform own_tr, Transform player_tr, float player_detect_distance, SpriteRenderer sr, float refresh_time, Action refresh_func) {
        this.rb = rb;
        move_speed = mv_spd;
        this.on_player_detect = on_player_detect;
        transform = own_tr;
        player_transform = player_tr;
        this.player_detect_distance = player_detect_distance;
        sprite_renderer = sr;
        this.refresh_time = refresh_time;
        on_refresh = refresh_func;
    }
    void IState.enter()
    {
        wander_delay = UnityEngine.Random.Range(wander_delay_timer_range.x, wander_delay_timer_range.y);
        dir_to_choose = choose_dir();
    }

    void IState.execute()
    {
        refresh_timer += Time.deltaTime;
        if (refresh_timer >= refresh_time) {
            refresh_timer = 0f;
            on_refresh();
        }
        wander_logic();

        Vector2 new_vel = dir_to_choose * move_speed;
        rb.velocity = new_vel;
        if (new_vel.x > 0) {
            sprite_renderer.flipX = false;
        } else if (new_vel.x < 0) {
            sprite_renderer.flipX = true;
        }
        if (Vector2.Distance(transform.position, player_transform.position) < player_detect_distance) {
            on_player_detect();
        }
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
        int dir = UnityEngine.Random.Range(1, 5);
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