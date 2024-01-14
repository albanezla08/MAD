using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenadeScript : MonoBehaviour
{
    public GameObject EnemyGrenadeChild;
    public PlayerScript player_script;
    public EnemyGrenadeChildScript child_script;
    public GameObject FireRingSpawner;
    public int child_dir;

    //wander
    public float speed;
    private float wander_delay;
    private float wander_delay_timer;
    private Vector3 dir_to_choose;
    private Vector3 old_dir;
    int dir;
    // Start is called before the first frame update
    void Start()
    {
        wander_delay = Random.Range(1.8f, 4.2f);
        wander_delay_timer = 0.0f;
        dir_to_choose = choose_dir();

        speed = 8.0f;
        player_script = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        child_dir = 0;
        //detonate();
    }

    // Update is called once per frame
    void Update()
    {
        wander_logic();
            
        wander(dir_to_choose);
    }

    void detonate() {
        Instantiate(FireRingSpawner, transform.position, transform.rotation);
        int i = 0;
        while (i < 4) {
            Instantiate(EnemyGrenadeChild, transform.position, transform.rotation);
            i++;
        }
        del_game_obj();
        
    }

    public void del_game_obj() {
        Destroy(gameObject);
    }
    public int get_child_dir() {
        return child_dir++ - 1;
    }

    void wander_logic() {
        if (wander_delay_timer >= wander_delay) {
            //Debug.Log("HIIIII");
            dir_to_choose = choose_dir();
            wander_delay_timer = 0;
        } else {
            wander_delay_timer += Time.deltaTime;
        }
    }

    void wander(Vector3 chosen_dir) {
        transform.position = transform.position + (chosen_dir * speed) * Time.deltaTime;
    }

    Vector3 choose_dir() {
        dir = Random.Range(1, 5);   // creates a number between 1 and 4
        //Debug.Log(dir);
        Vector3 wanted_vector;
        if (dir == 1) {
            wanted_vector = Vector3.up;
        } else if (dir == 2) {
            wanted_vector = Vector3.right;
        } else if (dir == 3) {
            wanted_vector = Vector3.down;
        } else {
            wanted_vector = Vector3.left;
        }
        return wanted_vector;
    }
}
