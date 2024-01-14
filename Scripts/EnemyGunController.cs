using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyGunController : EnemyController
{
    float shoot_at_player_range = 15f;
    float time_before_shooting = 2f;
    float shoot_speed = 15f;
    float shot_duration = 3f;
    protected override void change_to_chase()
    {
        Debug.Log(rb);
        state_machine.change_state(new ChaseState(rb, chase_speed, change_to_aiming, transform, player_transform, shoot_at_player_range));
        Debug.Log("change to chase");
    }

    void change_to_aiming()
    {
        state_machine.change_state(new AimingState(rb, transform, player_transform, time_before_shooting, shoot_speed, change_to_falling));
        Debug.Log("start aiming");
    }

    void change_to_falling()
    {
        Vector3 direction = rb.velocity.normalized;
        state_machine.change_state(new FallingState(rb, shoot_speed, direction, shot_duration, change_to_recovery));
    }
}
