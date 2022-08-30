using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public GameObject Flappy;
    private Rigidbody2D rigidbody2d;
    public float jump;
    public float tempo;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = Flappy.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jump;
        }
            if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Pulotempo());
        }
    }
    // Private para que ele fique somente nesse objeto
    // IEnumerator para criar uma coroutine 
    private IEnumerator Pulotempo()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        // está fazendo o ridigbody ficar no estado Kinematic 
        yield return new WaitForSeconds(tempo);
        // yield return está retornando para função o valor de tempo
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        // está fazendo o rigidbody voltar a ser Dynamic
    }

}
