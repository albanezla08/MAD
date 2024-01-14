using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player_transform;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player_transform.position.x, player_transform.position.y, transform.position.z);
    }
}
