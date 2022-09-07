using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // SerializeField para conseguir editar os valores dessas váriaveis no Inspector
    [SerializeField] private GameObject Flappy;
    [SerializeField] private float jump;
    [SerializeField] private float timeup;
    [SerializeField] private Transform flappydown;
    [SerializeField] private float timecount;

    private Rigidbody2D rigidbody2d;
    private float maxRotation = 25f;
    private float minRotation = -90f;
    private Transform localTrans;

    // OnValidate para usar o GetComponent
#if UNITY_EDITOR

    void OnValidate()
    {
        rigidbody2d = Flappy.GetComponent<Rigidbody2D>();
        localTrans = Flappy.GetComponent<Transform>();
    }


#endif


    // Update é chamado 1 vez por frame 
    void Update()
    {

        // Debug log para ver a velocidade que o flappy está caindo//  Debug.Log("velocity " + rigidbody2d.velocity.y);
        // Se apertar espaço ele executa as funções abaixo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumpflappy();
            RotateFlappy();
        }
        if (rigidbody2d.velocity.y < -7)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, flappydown.rotation, timecount * Time.deltaTime);
        }
        LimitRotation();
    }
    // faz o flappy pular
    private void Jumpflappy()
    {
        rigidbody2d.velocity = Vector2.up * jump;
    }

    // configura a rotação do flappy para 30 
    private void RotateFlappy()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 25);
    }

    private void LimitRotation()
    {
        Vector3 playerEulerAngles = localTrans.rotation.eulerAngles;

        playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, minRotation, maxRotation);

        localTrans.rotation = Quaternion.Euler(playerEulerAngles);

    }



}

