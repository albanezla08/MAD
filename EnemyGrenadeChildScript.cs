using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenadeChildScript : MonoBehaviour
{
    public float life_timer;
    public float life_time;
    public Vector3 movement_control;
    public float movement_speed;
    public int dir;
    public GameObject FireRingSpawner;
    public EnemyGrenadeScript parent_script;
    
    // Start is called before the first frame update
    void Start()
    {
        life_time = 0.25f;
        life_timer = 0.0f;
        movement_control = transform.position;
        movement_speed = 30.0f;
        parent_script = GameObject.FindWithTag("GrenadeParent").GetComponent<EnemyGrenadeScript>();
        dir = parent_script.get_child_dir();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = movement_control;
        if (dir == 0) {
            movement_control.y += movement_speed * Time.deltaTime;
        } else if (dir == 1) {
            movement_control.x += movement_speed * Time.deltaTime;
        } else if (dir == 2) {
            movement_control.y -= movement_speed * Time.deltaTime;
        } else if (dir == 3) {
            movement_control.x -= movement_speed * Time.deltaTime;
        }
        if (life_timer < life_time) {
            life_timer += Time.deltaTime;
        } else {
            explode();
        }
    }

    public void set_dir(int i) {
        dir = i;
    }

    public void explode() {
        Instantiate(FireRingSpawner, transform.position, transform.rotation);
        del_game_obj();
    }

    public void del_game_obj() {
        Destroy(gameObject);
    }
}
