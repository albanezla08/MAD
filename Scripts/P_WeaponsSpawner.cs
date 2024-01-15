using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_WeaponsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] pickup_options;
    public Vector3 bottom_left_bound = new Vector3(-90, -90, 0);
    public Vector3 top_right_bound = new Vector3(90, 90, 0);
    public Vector2 max_spawn_time_range = new Vector2(1.5f, 3f);
    public float min_distance_from_colliders = 0.5f;
    public float max_spawn_time;
    public float curr_spawn_time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        max_spawn_time = 3.0f; //Random.Range(max_spawn_time_range.x, max_spawn_time_range.y);
    }

    // Update is called once per frame
    void Update()
    {
        curr_spawn_time += Time.deltaTime;
        if (curr_spawn_time >= max_spawn_time) {
            curr_spawn_time = 0f;
            spawn_pickup();
        }
    }

    void spawn_pickup() {
        GameObject pickup_prefab = choose_pickup();
        Vector3 pickup_pos = choose_position();
        Instantiate(pickup_prefab, pickup_pos, Quaternion.identity);
    }

    GameObject choose_pickup() {
        return pickup_options[Random.Range(0, pickup_options.Length)];
    }

    Vector3 choose_position() {
        return choose_position_helper(3);
    }
    Vector3 choose_position_helper(int attempts) {
        Vector3 result = Vector3.zero;
        result.x = Random.Range(bottom_left_bound.x, top_right_bound.x);
        result.y = Random.Range(bottom_left_bound.y, top_right_bound.y);
        Collider2D col = Physics2D.OverlapCircle(result, min_distance_from_colliders);
        if (col != null && attempts > 0) {
            result = choose_position_helper(attempts - 1);
        }
        return result;
    }
}
