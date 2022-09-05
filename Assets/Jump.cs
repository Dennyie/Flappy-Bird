using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // SerializeField para conseguir editar os valores dessas váriaveis no Inspector
    [SerializeField]
    private GameObject Flappy;
    [SerializeField]
    private float jump;
    [SerializeField]
    private float time;
    [SerializeField]
    private float timeup;
    [SerializeField]
    private Transform flappydown;
    [SerializeField]
    private float timecount;

    private Rigidbody2D rigidbody2d;

    // OnValidate para usar o GetComponent
#if UNITY_EDITOR

void OnValidate()
    {
        rigidbody2d = Flappy.GetComponent<Rigidbody2D>();
    }

#endif

    // Update é chamado 1 vez por frame 
    void Update()
    {
        // Debug log para ver a velocidade que o flappy está caindo 
        //  Debug.Log("velocity " + rigidbody2d.velocity.y);
        // Se apertar espaço ele executa as funções abaixo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumpflappy();
            RotateFlappy();
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

    // faz o flappy pular
    private void Jumpflappy()
    {
        rigidbody2d.velocity = Vector2.up * jump;
    }
    
    // configura a rotação do flappy para 30 
    private void RotateFlappy()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 30);
    } 

   
    }

