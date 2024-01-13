using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;

public class GrenadeController : WeaponController
{
    void Start()
    {
        fire_speed = 5f;
    }
    protected override void fire_next() {
        GameObject next_weapon_prefab = next_weapons_prefabs[0];
        if (next_weapon_prefab == null) {
            Debug.Log("nothing to shoot");
        }
        GameObject weapon_object = Instantiate(next_weapon_prefab, transform.position, Quaternion.identity);
        Rigidbody2D weapon_body = weapon_object.GetComponent<Rigidbody2D>();
        WeaponController weapon_script = weapon_object.GetComponent<WeaponController>();
        weapon_body.velocity = point_dir * fire_speed;
        weapon_script.initialize(2, next_weapons_prefabs.Skip(1).ToArray(), point_dir);
    }
}
