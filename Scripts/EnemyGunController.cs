using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyGunController : EnemyController
{
    float shoot_at_player_range = 15f;
    protected override void change_to_chase()
    {
        Debug.Log(rb);
        state_machine.change_state(new ChaseState(rb, chase_speed, change_to_aiming, transform, player_transform, shoot_at_player_range));
        Debug.Log("change to chase");
    }

    void change_to_aiming()
    {
        state_machine.change_state(new AimingState(rb, transform, player_transform));
        Debug.Log("start aiming");
    }
}
