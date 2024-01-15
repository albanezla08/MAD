using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// [System.Serializable] public class WeaponsQueueControllerUnityEvent:UnityEvent<WeaponsQueueController> {}

public class PlayerScript : MonoBehaviour
{
    public GameManagerScript gm_script;

    //base
    public Rigidbody2D body;
    public SpriteRenderer sprite_renderer;

    //basic movement
    public Vector2 control_velocity;
    public float active_movement_rate;
    public float base_movement_rate;

    //sprint
    public float sprint_movement_rate;
    private float sprint_timer;
    private float sprint_time;

    //colliding
    public bool hit_by_enemy;
    public float hit_timer;
    public float hit_time;
    public Vector3 hit_dir;
    public float hit_speed;

    //weapon
    private WeaponsQueueController weapon_queue_script;
    private float fire_speed = 5f;
    //events for UI
    public UnityEvent<WeaponsQueueController> queue_changed;
    public UnityEvent<int> health_changed;
    //animations
    public Animator own_animator;
    //audio
    public AudioManagerScript audio_manager_script;
    //health
    [SerializeField] int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        gm_script = GameObject.FindWithTag("game_manager").GetComponent<GameManagerScript>();

        //base
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        own_animator = gameObject.GetComponent<Animator>();
        gameObject.name = "Player";
        body.gravityScale = 0;

        //basic movement
        control_velocity = new Vector2(0.0f, 0.0f);
        base_movement_rate = 30.0f * 0.7f;
        active_movement_rate = base_movement_rate;

        //sprint
        sprint_movement_rate = 50.0f * 0.7f;
        sprint_timer = 0.0f;
        sprint_time = 0.0f;

        //weapons
        weapon_queue_script = gameObject.GetComponent<WeaponsQueueController>();
        queue_changed.Invoke(weapon_queue_script);

        hit_by_enemy = false;
        hit_timer = 0.0f;
        hit_time = 0.35f;
        hit_dir = new Vector3(0.0f, 0.0f, 0.0f);
        hit_speed = 15.0f;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = control_velocity;
        if (!hit_by_enemy) {
            if (body.velocity.x > 0) {
                sprite_renderer.flipX = true;
            } else if (body.velocity.x < 0) {
                sprite_renderer.flipX = false;
            }
            own_animator.SetBool("isWalking", body.velocity.magnitude > 0.01);
            //basic movement
            // control_velocity = po_script.check_overlap(control_velocity);
            

            //basic movement
            if (Input.GetKey(KeyCode.W)) {
                control_velocity.y = active_movement_rate;

            } else if (control_velocity.y > 0){
                control_velocity.y = 0;
            }
            if (Input.GetKey(KeyCode.A)) {
                control_velocity.x = -active_movement_rate;

            } else if (control_velocity.x < 0){
                control_velocity.x = 0;
            }
            if (Input.GetKey(KeyCode.S)) {
                control_velocity.y = -active_movement_rate;

            } else if (control_velocity.y < 0){
                control_velocity.y = 0;
            }
            if (Input.GetKey(KeyCode.D)) {
                control_velocity.x = active_movement_rate;

            } else if (control_velocity.x > 0){
                control_velocity.x = 0;
            }

            //double movement key inputs and correcting:
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) {
                control_velocity.y = (float)((active_movement_rate / 2) * System.Math.Sqrt(2));
                control_velocity.x = (float)((active_movement_rate / 2) * System.Math.Sqrt(2));

            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) {
                control_velocity.y = (float)((active_movement_rate / 2) * System.Math.Sqrt(2));
                control_velocity.x = (float)(-(active_movement_rate / 2) * System.Math.Sqrt(2));

            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
                control_velocity.y = (float)(-(active_movement_rate / 2) * System.Math.Sqrt(2));
                control_velocity.x = (float)(-(active_movement_rate / 2) * System.Math.Sqrt(2));

            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
                control_velocity.y = (float)(-(active_movement_rate / 2) * System.Math.Sqrt(2));
                control_velocity.x = (float)((active_movement_rate / 2) * System.Math.Sqrt(2));

            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
                control_velocity.x = 0;
                control_velocity.y = 0;

            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) {
                control_velocity.x = 0;
                control_velocity.y = 0;

            }

            //weapon controls
            if (Input.GetMouseButtonDown(0)) {
                fire_weapon();
            }

            //sprint
            if (sprint_timer < sprint_time) {
                sprint_timer += Time.deltaTime;
            } else if (sprint_timer != 0.0f) {
                exit_sprint();
            } else {
                //do nothing
            }
        } else {
            if (hit_timer < hit_time) {
                hit_timer += Time.deltaTime;
                control_velocity = 5 * hit_dir;

            } else {
                hit_by_enemy = false;
                hit_timer = 0.0f;
            }
        }

    }

    //sprint
    public void enter_sprint(float input_sprint_time) {

        sprint_timer = 0.0f;
        active_movement_rate = sprint_movement_rate;
        if (input_sprint_time > (sprint_time - sprint_timer)) {
            sprint_time = input_sprint_time;
        } else {
            //do nothing
        } 
    }
    public void exit_sprint() {
        active_movement_rate = base_movement_rate;
        sprint_time = 0.0f;
        sprint_timer = 0.0f;

    }

    public void reset_speed() {
        active_movement_rate = base_movement_rate;
    }

    public bool check_sprint_active() {
        if (active_movement_rate == sprint_movement_rate) {
            return true;
        } else {
            return false;
        }
    }

    //base functions
    public void del_game_obj() {
        Destroy(gameObject);
    }
    public float get_x_pos() {
        return transform.position.x;
    }
    public float get_y_pos() {
        return transform.position.y;
    }
    public void debug_check() {
        Debug.Log("Test");
    }

    //weapon functions
    private void fire_weapon() {
        GameObject next_weapon_prefab = weapon_queue_script.pop_next_weapon();
        queue_changed.Invoke(weapon_queue_script);
        if (next_weapon_prefab == null) {
            own_animator.SetTrigger("Attack");
            audio_manager_script.play_clip("Empty Shoot");
            gm_script.show_need_weapon();
            // Debug.Log("nothing to shoot");
            return;
        }
        GameObject weapon_object = Instantiate(next_weapon_prefab, transform.position, Quaternion.identity);
        Rigidbody2D weapon_body = weapon_object.GetComponent<Rigidbody2D>();
        WeaponController weapon_script = weapon_object.GetComponent<WeaponController>();
        Vector3 shoot_dir = calc_direction().normalized;
        weapon_body.velocity = shoot_dir * fire_speed;
        weapon_script.initialize(2, weapon_queue_script, shoot_dir, gameObject);
        weapon_queue_script.clear();
        queue_changed.Invoke(weapon_queue_script);
    }
    private Vector3 calc_direction() {
        Vector3 playerPos = transform.position;
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        return cursorPos - playerPos;
    }

    //weapon pickups
    void OnTriggerEnter2D(Collider2D col) {
        PickupIdentifier pickupIdentifier = col.GetComponent<PickupIdentifier>();
        if (pickupIdentifier != null) {
            bool added_weapon = weapon_queue_script.add_weapon(pickupIdentifier.get_weapon_prefab());
            queue_changed.Invoke(weapon_queue_script);
            if (added_weapon) {
                Destroy(col.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == 7) {
            hit_by_enemy = true;
            hit_timer = 0.0f;
            hit_dir = transform.position - col.transform.position;
            hp--;
            health_changed.Invoke(hp);
            if (hp <= 0) {
                gm_script.game_over();
            }
        }
    }
    
}
