using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeVelocity : MonoBehaviour
{
    [SerializeField]    public float speed = 5f;
    [SerializeField]    private float leftEdge;


    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

}
