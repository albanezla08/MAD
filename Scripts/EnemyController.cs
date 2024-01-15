using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour, IDamageable
{
    protected StateMachine state_machine = new StateMachine();
    protected IState wander_state;
    protected Rigidbody2D rb;
    protected Transform player_transform;
    protected SpriteRenderer sprite_renderer;
    [SerializeField] protected float move_speed = 2f;
    [SerializeField] protected float chase_speed = 3f;
    [SerializeField] protected float fall_speed = 2f;
    [SerializeField] protected float recovery_time;
    [SerializeField] protected float player_detect_distance = 10f;
    [SerializeField] protected int hp = 5;
    [SerializeField] protected GameObject drop_prefab;
    protected bool is_dead = false;
    protected float fall_time;
    protected SpriteRenderer exclamation_renderer;
    protected SpriteRenderer stars_renderer;
    // used to make sure scaling difficulty speed is reflected in wander speed
    float wander_speed_refresh_time = 6f;
    public UnityEvent death_event;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        gm.GetComponent<GameManagerScript>().difficulty_event.AddListener(incr_speeds);
        rb = gameObject.GetComponent<Rigidbody2D>();
        player_transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        exclamation_renderer = transform.Find("Exclamation").GetComponent<SpriteRenderer>();
        stars_renderer = transform.Find("Stars").GetComponent<SpriteRenderer>();
        state_machine.change_state(new WanderState(rb, move_speed, change_to_chase, transform, player_transform, player_detect_distance, sprite_renderer, wander_speed_refresh_time, change_to_wander));
    }

    // Update is called once per frame
    void Update()
    {
        state_machine.update();
    }

    protected void change_to_recovery() {
        state_machine.change_state(new RecoveryState(rb, recovery_time, change_to_wander, stars_renderer));
    }

    protected void change_to_wander() {
        state_machine.change_state(new WanderState(rb, move_speed, change_to_chase, transform, player_transform, player_detect_distance, sprite_renderer, wander_speed_refresh_time, change_to_wander));
    }

    protected virtual void change_to_chase() {
        state_machine.change_state(new ChaseState(rb, chase_speed, ()=>Debug.Log("got you!"), transform, player_transform, player_detect_distance, sprite_renderer, exclamation_renderer, change_to_wander));
    }

    void IDamageable.on_hit(int damage, Vector3 direction) {
        hp -= damage;
        if (hp <= 0) {
            die();
            return;
        }
        fall_time = 1.5f;

        state_machine.change_state(new FallingState(rb, fall_speed, direction, fall_time, change_to_recovery));
    }

    bool IDamageable.is_alive() {
        return !is_dead;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == 6) {
            // hit player
            // change_to_recovery();
            StartCoroutine(switch_at_end_of_frame());
        }
    }

    IEnumerator switch_at_end_of_frame() {
        yield return new WaitForEndOfFrame();
        change_to_recovery();
    }

    void die() {
        if (is_dead) {
            return;
        }
        is_dead = true;
        death_event.Invoke();
        Instantiate(drop_prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void incr_player_detector() {
        player_detect_distance += 1f;
        move_speed += 0.25f;
        chase_speed += 0.25f;
    }

    public void incr_speeds() {
        player_detect_distance += 1f;
        move_speed += 0.25f;
        chase_speed += 0.25f;
    }
}
