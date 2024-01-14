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
    public ChaseState(Rigidbody2D rb, float chs_spd, Action on_player_in_range, Transform own_tr, Transform player_tr, float player_in_range_distance) {
        this.rb = rb;
        chase_speed = chs_spd;
        this.on_player_in_range = on_player_in_range;
        transform = own_tr;
        player_transform = player_tr;
        this.player_in_range_distance = player_in_range_distance;
    }
    void IState.enter()
    {
        Vector2 dir = player_transform.position - transform.position;
        dir = dir.normalized;
        rb.velocity = dir * chase_speed;
    }

    void IState.execute()
    {
        Vector2 dir = player_transform.position - transform.position;
        dir = dir.normalized;
        rb.velocity = dir * chase_speed;
        if (Vector2.Distance(transform.position, player_transform.position) < player_in_range_distance) {
            on_player_in_range();
        }
    }

    void IState.exit()
    {

    }
}