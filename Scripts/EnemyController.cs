using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    StateMachine state_machine = new StateMachine();
    IState wander_state;
    Rigidbody2D rb;
    [SerializeField] Transform player_transform;
    [SerializeField] float move_speed;
    [SerializeField] float recovery_time;
    [SerializeField] float player_detect_distance = 10f;
    float fall_time;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        wander_state = new WanderState(rb, move_speed, change_to_chase, transform, player_transform, player_detect_distance);
        state_machine.change_state(wander_state);
    }

    // Update is called once per frame
    void Update()
    {
        state_machine.update();
    }

    void change_to_recovery() {
        state_machine.change_state(new RecoveryState(rb, recovery_time, change_to_wander));
    }

    void change_to_wander() {
        state_machine.change_state(wander_state);
    }

    void change_to_chase() {
        state_machine.change_state(new ChaseState(rb, move_speed, ()=>Debug.Log("got you!"), transform, player_transform, player_detect_distance));
    }

    void IDamageable.on_hit(int damage, Vector3 direction) {
        fall_time = 1.5f;

        state_machine.change_state(new FallingState(rb, move_speed, direction, fall_time, change_to_recovery));
    }
}
