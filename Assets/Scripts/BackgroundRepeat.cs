using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    [SerializeField] private float width;
    [SerializeField] private float speed = -3f;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rb;

#if UNITY_EDITOR

    void OnValidate()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        width = boxCollider.size.x;
    }


#endif

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, 0);
    }

    void Update()
    {
        if (transform.position.x < -width)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        Vector2 vector = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + vector;
    }
}
