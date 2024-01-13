using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected int next_weapons_count;
    protected GameObject[] next_weapons_prefabs;
    public void initialize(int count, GameObject[] prefabs) {
        next_weapons_count = count;
        next_weapons_prefabs = prefabs;
    }

    protected virtual void fire_next() {

    }
}
