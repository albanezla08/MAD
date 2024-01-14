using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupIdentifier : MonoBehaviour
{
    [SerializeField] GameObject weapon_prefab;
    public GameObject get_weapon_prefab(){
        return weapon_prefab;
    }
}
