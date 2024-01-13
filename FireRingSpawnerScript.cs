using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRingSpawnerScript : MonoBehaviour
{
    public GameObject FireRing;
    public float spawn_rate;
    public float spawn_timer;
    public int num_spawns;
    public int spawn_counter;
    // Start is called before the first frame update
    void Start()
    {
        spawn_rate = 0.05f;
        spawn_timer = 0.0f;
        num_spawns = 20;
        spawn_counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn_timer < spawn_rate && spawn_counter < num_spawns) {
            spawn_timer += Time.deltaTime;
        } else if (spawn_counter < num_spawns) {
            spawn_timer = 0.0f;
            spawn_counter ++;
            Instantiate(FireRing, transform.position, transform.rotation);
        } else {
            del_game_obj();
        }
    }
    public void del_game_obj() {
        Destroy(gameObject);
    }
}
