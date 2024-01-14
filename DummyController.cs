using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour, IDamageable
{
    Rigidbody2D rb;
    enum BadStates {Falling, Recovering, Returning, Idle};
    BadStates currentBadState = BadStates.Idle;
    Vector3 starting_pos;
    float walk_speed = 1f;
    float max_recover_time = 1f;
    float curr_recover_time = 0f;
    float max_fall_time = 0f;
    float curr_fall_time = 0f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        starting_pos = transform.position;
    }
    void Update()
    {
        switch (currentBadState) {
            case BadStates.Idle:
                break;
            case BadStates.Returning:
                // transform.position = Vector2.MoveTowards(transform.position, starting_pos, walk_speed * Time.deltaTime);
                rb.velocity = (starting_pos - transform.position) * walk_speed;
                if (transform.position.magnitude - starting_pos.magnitude < 0.01) {
                    rb.velocity = Vector2.zero;
                    currentBadState = BadStates.Idle;
                }
                break;
            case BadStates.Recovering:
                curr_recover_time += Time.deltaTime;
                if (curr_recover_time >= max_recover_time) {
                    curr_recover_time = 0f;
                    currentBadState = BadStates.Returning;
                }
                break;
            case BadStates.Falling:
                curr_fall_time += Time.deltaTime;
                if (curr_fall_time >= max_fall_time) {
                    curr_fall_time = 0f;
                    rb.velocity = Vector2.zero;
                    currentBadState = BadStates.Recovering;
                }
                break;
        }
    }
    void IDamageable.on_hit(int damage, Vector3 direction) {
        Debug.Log("ow");
        curr_recover_time = 0f;
        rb.velocity += (Vector2)direction;
        max_fall_time += 0.5f;
        curr_fall_time = 0f;
        currentBadState = BadStates.Falling;
    }
}
