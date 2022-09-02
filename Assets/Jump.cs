using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public GameObject Flappy;
    private Rigidbody2D rigidbody2d;
    public float jump;
    public float time;
    public float timeup;
    public Transform flappydown;
    public float timecount;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = Flappy.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("velocity " + rigidbody2d.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumpflappy();
            Scaleflappy();
            StartCoroutine(JumptimeRoutine());
        }
        if (rigidbody2d.velocity.y < -7)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, flappydown.rotation, timecount * Time.deltaTime);
        } 
    }
    // Private para que ele fique somente nesse objeto
    // IEnumerator para criar uma coroutine 
    private IEnumerator JumptimeRoutine()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        // está fazendo o ridigbody ficar no estado Kinematic 
        yield return new WaitForSeconds(time);
        // yield return está retornando para função o valor de tempo
        //  StartCoroutine(RotationRoutine());
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        // está fazendo o rigidbody voltar a ser Dynamic
    }

    private void Jumpflappy()
    {
        rigidbody2d.velocity = Vector2.up * jump;
    }

    private void Scaleflappy()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 30);
    } 

   
    }

