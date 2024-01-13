using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected int next_weapons_count;
    protected GameObject[] next_weapons_prefabs;
    protected Vector3 point_dir;
    protected float fire_speed = 1f;
    public void initialize(int count, GameObject[] prefabs, Vector3 direction) {
        next_weapons_count = count;
        next_weapons_prefabs = prefabs;
        point_dir = direction;
    }

    protected virtual void fire_next() {

    }
}
