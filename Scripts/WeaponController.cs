using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected int next_weapons_count;
    protected WeaponsQueueController weapon_queue_script;
    protected Vector3 point_dir;
    protected float fire_speed = 1f;
    protected int damage = 1;
    protected virtual void Start()
    {
        CircleCollider2D ownCollider = gameObject.GetComponent<CircleCollider2D>();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, ownCollider.radius);
        Debug.Log("into hit colldiers");
        Debug.Log(hitColliders.Length);
        Debug.Log("out of hit colldiers");
    }
    public void initialize(int count, WeaponsQueueController source_queue, Vector3 direction) {
        next_weapons_count = count;
        weapon_queue_script = gameObject.GetComponent<WeaponsQueueController>();
        weapon_queue_script.copy_queue(source_queue);
        point_dir = direction;
    }

    protected virtual void fire_next() {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        MonoBehaviour[] monoBehaviours = col.GetComponents<MonoBehaviour>();
        Debug.Log(monoBehaviours[0]);
        for (int i = 0; i < monoBehaviours.Length; i++) {
            if (monoBehaviours[i] is IDamageable) {
                IDamageable script = (IDamageable)monoBehaviours[i];
                script.on_hit(damage, point_dir);
                fire_next();
                Destroy(gameObject);
                break;
            }
        }
    }
}
