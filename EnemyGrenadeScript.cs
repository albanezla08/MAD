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
    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        child_dir = 0;
        detonate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void detonate() {
        //instnatiate fire ring spawner
        //this will have a detector collider and if that detects
        //another grenade it will initiate an explosion
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
}
