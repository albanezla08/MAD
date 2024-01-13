using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    private int next_weapons_count;
    private GameObject[] next_weapons_prefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int count, GameObject[] prefabs) {
        next_weapons_count = count;
        Debug.Log(prefabs);
    }
}
