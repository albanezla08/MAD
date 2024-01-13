using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private int next_weapons_count;
    private GameObject[] next_weapons_prefabs;
    public void initialize(int count, GameObject[] prefabs) {
        next_weapons_count = count;
        Debug.Log(prefabs);
    }
}
