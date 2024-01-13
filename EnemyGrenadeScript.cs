using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenadeScript : MonoBehaviour
{
    public GameObject EnemyGrenadeChild;
    public PlayerScript player_script;
    public EnemyGrenadeChildScript child_script;
    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void detonate() {
        //instnatiate fire ring spawner
        //this will have a detector collider and if that detects
        //another grenade it will initiate an explosion
        
        int i = 0;
        while (i < 4) {
            Instantiate(EnemyGrenadeChild, transform.position, transform.rotation);
            child_script = GameObject.FindWithTag("GrenadeChild").GetComponent<EnemyGrenadeChildScript>();
            child_script.set_dir(i);
            i++;

        }
        
    }
}
