using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirstController : EnemyController
{
    [SerializeField] float must_have_hit_range;
    protected override void change_to_chase()
    {
        state_machine.change_state(new ChaseState(rb, chase_speed, change_to_recovery, transform, player_transform, must_have_hit_range, sprite_renderer));
    }
}
