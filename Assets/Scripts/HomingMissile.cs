using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Transform target;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();       
    }

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.up = target.position - transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }
}
