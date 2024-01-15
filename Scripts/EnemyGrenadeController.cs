using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenadeController : EnemyController
{
    public GameObject FireRingSpawner;
    public GameObject EnemyGrenadeChild;
    [SerializeField] float explode_player_distance = 5f;
    [SerializeField] float detonate_anim_time = 2.2f;
    private Animator own_animator;
    protected override void change_to_chase() {
        state_machine.change_state(new ChaseState(rb, chase_speed, change_to_detonating, transform, player_transform, explode_player_distance, sprite_renderer, exclamation_renderer));
    }
    void change_to_detonating() {
        own_animator = gameObject.GetComponent<Animator>();
        state_machine.change_state(new DetonatingState(rb, detonate_anim_time, detonate, own_animator));
    }
    void detonate() {
        Instantiate(FireRingSpawner, transform.position, transform.rotation);
        int i = 0;
        while (i < 4) {
            GameObject child = Instantiate(EnemyGrenadeChild, transform.position, transform.rotation);
            child.GetComponent<EnemyGrenadeChildScript>().set_dir(i);
            i++;
        }
        Destroy(gameObject);
        
    }
}
