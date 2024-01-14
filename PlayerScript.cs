using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// [System.Serializable] public class WeaponsQueueControllerUnityEvent:UnityEvent<WeaponsQueueController> {}

public class PlayerScript : MonoBehaviour
{
    //base
    public Rigidbody2D body;

    //basic movement
    public Vector2 control_velocity;
    public float active_movement_rate;
    public float base_movement_rate;

    //sprint
    public float sprint_movement_rate;
    private float sprint_timer;
    private float sprint_time;

    //weapon
    private WeaponsQueueController weapon_queue_script;
    private float fire_speed = 5f;
    //collision/movement
    private PreventOverlap po_script;
    //events for UI
    public UnityEvent<WeaponsQueueController> queue_changed;

    // Start is called before the first frame update
    void Start()
    {

        //base
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

        //collision/movement
        po_script = gameObject.GetComponent<PreventOverlap>();
    }

    // Update is called once per frame
    void Update()
    {

        //basic movement
        // control_velocity = po_script.check_overlap(control_velocity);
        body.velocity = control_velocity;

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
            Debug.Log("nothing to shoot");
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
    
}
