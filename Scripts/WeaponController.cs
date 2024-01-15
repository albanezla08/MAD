using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected int next_weapons_count;
    protected WeaponsQueueController weapon_queue_script;
    protected Vector3 point_dir;
    [SerializeField] protected float fire_speed = 1f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float duration = 3f;
    protected GameObject firer;
    protected Rigidbody2D rb;
    protected bool set_to_destroy = false;
    protected AudioManagerScript audio_manager_script;
    protected virtual void Start()
    {
        // CircleCollider2D ownCollider = gameObject.GetComponent<CircleCollider2D>();
        // Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, ownCollider.radius);
        rb = gameObject.GetComponent<Rigidbody2D>();
        audio_manager_script = FindObjectOfType<AudioManagerScript>();
    }
    public void initialize(int count, WeaponsQueueController source_queue, Vector3 direction, GameObject owner) {
        next_weapons_count = count;
        weapon_queue_script = gameObject.GetComponent<WeaponsQueueController>();
        weapon_queue_script.copy_queue(source_queue);
        point_dir = direction;
        firer = owner;
    }

    protected virtual void fire_next() {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (set_to_destroy) {
            return;
        }
        MonoBehaviour[] monoBehaviours = col.GetComponents<MonoBehaviour>();
        for (int i = 0; i < monoBehaviours.Length; i++) {
            if (monoBehaviours[i].gameObject == firer) {
                continue;
            }
            if (monoBehaviours[i] is IDamageable) {
                IDamageable script = (IDamageable)monoBehaviours[i];
                script.on_hit(damage, point_dir);
                fire_next();
                audio_manager_script.play_clip("Enemy Hit");
                StartCoroutine(delayed_destroy());
                break;
            } else if (monoBehaviours[i].gameObject.layer == LayerMask.NameToLayer("Projectiles")) {
                StartCoroutine(delayed_destroy());
                fire_next();
                break;
            }
        }
    }

    protected IEnumerator delayed_destroy() {
        if (set_to_destroy) {
            yield break;
        }
        set_to_destroy = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        if (gameObject != null) {
            Destroy(gameObject);
        }
    }
}
