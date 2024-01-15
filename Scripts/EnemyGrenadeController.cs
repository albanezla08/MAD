using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenadeController : EnemyController
{
    public GameObject FireRingSpawner;
    public GameObject EnemyGrenadeChild;
    [SerializeField] float explode_player_distance = 2f;
    protected override void change_to_chase() {
        state_machine.change_state(new ChaseState(rb, chase_speed, detonate, transform, player_transform, explode_player_distance));
    }
    void detonate() {
        Instantiate(FireRingSpawner, transform.position, transform.rotation);
        int i = 0;
        while (i < 4) {
            Instantiate(EnemyGrenadeChild, transform.position, transform.rotation);
            i++;
        }
        Destroy(gameObject);
        
    }
}
