using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollectedDetector : MonoBehaviour
{
    public int id;
    public GameObject pickup_prefab;
    private bool coroutine_running;
    private bool spawn_queued = false;
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            StartCoroutine(check_later());
            coroutine_running = true;
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        if (spawn_queued && col.CompareTag("Player")) {
            spawn_pickup();
            spawn_queued = false;
        }
    }
    IEnumerator check_later() {
        // if (coroutine_running) {
        //     yield break;
        // }
        yield return new WaitForSeconds(0.1f);
        int children_count = transform.childCount;
        // Debug.Log(children_count);
        if (children_count == 0) {
            queue_spawn();
        }
        // coroutine_running = false;
    }
    void queue_spawn() {
        spawn_queued = true;
    }
    void spawn_pickup() {
        GameObject obj = Instantiate(pickup_prefab, transform.position, Quaternion.identity);
        obj.transform.parent = gameObject.transform;
    }
}
