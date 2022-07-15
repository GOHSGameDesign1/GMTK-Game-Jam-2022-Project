using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float runSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector2 newPos = rb.position + (Vector2)transform.right * -1f * runSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }
}
