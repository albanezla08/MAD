using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventOverlap : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    Transform tr;
    Func<Vector3, Vector3> shape_check;
    [SerializeField] LayerMask layer_mask;
    [SerializeField] float stop_distance = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        tr = gameObject.GetComponent<Transform>();
        col = gameObject.GetComponent<Collider2D>();
        if (col is CircleCollider2D) {
            shape_check = circle_check;
        } else if (col is CapsuleCollider2D) {
            shape_check = capsule_check;
        } else if (col is BoxCollider2D) {
            shape_check = box_check;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = check_overlap(rb.velocity);
    }
    public Vector3 check_overlap(Vector3 move_dir) {
        return shape_check(move_dir);
    }
    // circle
    Vector3 circle_check(Vector3 source) {
        CircleCollider2D circle_col = (CircleCollider2D)col;
        float radius = circle_col.radius;
        Vector3 curr_pos = tr.position;
        Vector2 curr_vel = rb.velocity;
        Func<Collider2D> right_check = () => Physics2D.OverlapCircle(curr_pos + (Vector3.right * stop_distance), radius);
        Func<Collider2D> left_check = () => Physics2D.OverlapCircle(curr_pos + (Vector3.left * stop_distance), radius);
        Func<Collider2D> up_check = () => Physics2D.OverlapCircle(curr_pos + (Vector3.up * stop_distance), radius);
        Func<Collider2D> down_check = () => Physics2D.OverlapCircle(curr_pos + (Vector3.down * stop_distance), radius);
        return cancel_movement(source, right_check, left_check, up_check, down_check);
    }
    // box
    public Vector3 box_check(Vector3 source) {
        BoxCollider2D box_col = (BoxCollider2D)col;
        Vector2 size = box_col.size;
        Vector3 curr_pos = tr.position;
        Vector2 curr_vel = rb.velocity;
        Func<Collider2D> right_check = () => Physics2D.OverlapBox(curr_pos + (Vector3.right * stop_distance), size, 0f);
        Func<Collider2D> left_check = () => Physics2D.OverlapBox(curr_pos + (Vector3.left * stop_distance), size, 0f);
        Func<Collider2D> up_check = () => Physics2D.OverlapBox(curr_pos + (Vector3.up * stop_distance), size, 0f);
        Func<Collider2D> down_check = () => Physics2D.OverlapBox(curr_pos + (Vector3.down * stop_distance), size, 0f);
        return cancel_movement(source, right_check, left_check, up_check, down_check);
    }
    // capsule
    public Vector3 capsule_check(Vector3 source) {
        CapsuleCollider2D capsule_col = (CapsuleCollider2D)col;
        Vector2 size = capsule_col.size;
        CapsuleDirection2D direction = capsule_col.direction;
        Vector3 curr_pos = tr.position;
        // Vector2 curr_vel = rb.velocity;
        Func<Collider2D> right_check = () => Physics2D.OverlapCapsule(curr_pos + (Vector3.right * stop_distance), size, direction, 0f, layer_mask);
        Func<Collider2D> left_check = () => Physics2D.OverlapBox(curr_pos + (Vector3.left * stop_distance), size, 0f, layer_mask);
        Func<Collider2D> up_check = () => Physics2D.OverlapBox(curr_pos + (Vector3.up * stop_distance), size, 0f, layer_mask);
        Func<Collider2D> down_check = () => Physics2D.OverlapBox(curr_pos + (Vector3.down * stop_distance), size, 0f, layer_mask);
        return cancel_movement(source, right_check, left_check, up_check, down_check);
    }
    // generic
    Vector3 cancel_movement(Vector3 vec, Func<Collider2D> right, Func<Collider2D> left, Func<Collider2D> up, Func<Collider2D> down) {
        Vector3 result = vec;
        // Debug.Log(right());
        gameObject.layer = LayerMask.NameToLayer("Default");
        if (vec.x > 0 && right() != null) {
            result.x = 0;
        } else if (vec.x < 0 && left() != null) {
            result.x = 0;
        }
        if (vec.y > 0 && up() != null) {
            result.y = 0;
        } else if (vec.y < 0 && down() != null) {
            result.y = 0;
        }
        gameObject.layer = LayerMask.NameToLayer("Takes Space");
        return result;
    }
}
