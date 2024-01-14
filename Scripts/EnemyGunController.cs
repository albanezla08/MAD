using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    StateMachine state_machine = new StateMachine();
    IState wander_state;
    Rigidbody2D rb;
    [SerializeField] float move_speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        wander_state = new WanderState(rb, move_speed);
        state_machine.change_state(wander_state);
    }

    // Update is called once per frame
    void Update()
    {
        state_machine.update();
    }
}
