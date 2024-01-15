using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyGunController : EnemyController
{
    [SerializeField] float shoot_at_player_range = 15f;
    [SerializeField] float time_before_shooting = 2f;
    [SerializeField] float shoot_speed = 5f;
    [SerializeField] float shot_duration = 3f;
    protected override void change_to_chase()
    {
        state_machine.change_state(new ChaseState(rb, chase_speed, change_to_aiming, transform, player_transform, shoot_at_player_range, sprite_renderer, exclamation_renderer, change_to_wander));
    }

    void change_to_aiming()
    {
        state_machine.change_state(new AimingState(rb, transform, player_transform, time_before_shooting, shoot_speed, change_to_falling));
    }

    void change_to_falling()
    {
        Vector3 direction = rb.velocity.normalized;
        state_machine.change_state(new FallingState(rb, shoot_speed, direction, shot_duration, change_to_recovery));
    }
}
