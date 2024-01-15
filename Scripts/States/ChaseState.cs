using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    float chase_speed;
    Rigidbody2D rb;
    Action on_player_in_range;
    Transform transform, player_transform;
    float player_in_range_distance;
    SpriteRenderer sprite_renderer;
    SpriteRenderer exclamation_renderer;
    float give_up_time = 5f;
    float give_up_timer = 0f;
    Action on_give_up;
    public ChaseState(Rigidbody2D rb, float chs_spd, Action on_player_in_range, Transform own_tr, Transform player_tr, float player_in_range_distance, SpriteRenderer sr, SpriteRenderer exclamation_renderer, Action give_up) {
        this.rb = rb;
        chase_speed = chs_spd;
        this.on_player_in_range = on_player_in_range;
        transform = own_tr;
        player_transform = player_tr;
        this.player_in_range_distance = player_in_range_distance;
        sprite_renderer = sr;
        this.exclamation_renderer = exclamation_renderer;
        on_give_up = give_up;
    }
    void IState.enter()
    {
        Vector2 dir = player_transform.position - transform.position;
        dir = dir.normalized;
        rb.velocity = dir * chase_speed;
        exclamation_renderer.enabled = true;
        give_up_timer = 0f;
    }

    void IState.execute()
    {
        give_up_timer += Time.deltaTime;
        if (give_up_timer >= give_up_time) {
            on_give_up();
        }
        Vector2 dir = player_transform.position - transform.position;
        dir = dir.normalized;
        Vector2 new_vel = dir * chase_speed;
        rb.velocity = new_vel;
        if (new_vel.x > 0) {
            sprite_renderer.flipX = false;
        } else if (new_vel.x < 0) {
            sprite_renderer.flipX = true;
        }
        if (Vector2.Distance(transform.position, player_transform.position) < player_in_range_distance) {
            on_player_in_range();
        }
    }

    void IState.exit()
    {
        exclamation_renderer.enabled = false;
    }
}