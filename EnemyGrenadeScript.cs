using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenadeScript : MonoBehaviour
{

    public PlayerScript player_script;
    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
