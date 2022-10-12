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

    [SerializeField] private Rigidbody2D rigidbody2d;
    private float maxRotation = 25f;
    private float minRotation = -90f;
    [SerializeField] private Transform localTrans;


    // OnValidate para usar o GetComponent
#if UNITY_EDITOR    // A utilização do unity_editor é para que não haja problemas em build

    void OnValidate()   //// Onvalidate só roda em editor
    {
        rigidbody2d = Flappy.GetComponent<Rigidbody2D>();
        localTrans = Flappy.GetComponent<Transform>();
    }


#endif

    void Update() // Update é chamado 1 vez por frame 
    {

        //  Debug.Log("velocity " + rigidbody2d.velocity.y);    // Debug log para ver a velocidade que o flappy está caindo
        if (Input.GetKeyDown(KeyCode.Space))    // Se apertar espaço ele executa as funções abaixo
        {
            rigidbody2d.bodyType = RigidbodyType2D.Dynamic; //  Apertar espaço faz com que o flappy deixe de ser Kinematic e volte a ser Dynamic, sendo afetado pela gravidade e voltando a poder cair normalmente 
            Jumpflappy();        // Chama a função Jumpflappy
            RotateFlappy();     // Chama a função RotateFlappy
        }
        if (rigidbody2d.velocity.y < -7)    // Se a velocidade Y do rigidbody2d for menor que -7 ele executa a função abaixo
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, flappydown.rotation, timecount * Time.deltaTime);   // A função quaternion.slerp faz o flappy iniciar uma rotação 
        }
        LimitRotation();    // Função para limitar a rotação do flappy
    }
    
    private void Jumpflappy()   // Faz o flappy pular
    {
        rigidbody2d.velocity = Vector2.up * jump;   // Muda a velocidade do rigidbody2d para fazer o flappy pular
    }

    // configura a rotação do flappy para 30 
    private void RotateFlappy() // Função para fazer o flappy ficar com bico levantado toda vez que apertamos espaço
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 25);   
    }

    private void LimitRotation()    // Clamp para não deixar o flappy rotacionar mais do que o normal
    {
        Vector3 playerEulerAngles = localTrans.rotation.eulerAngles;

        playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, minRotation, maxRotation);

        localTrans.rotation = Quaternion.Euler(playerEulerAngles);

    }

    private void OnTriggerEnter2D(Collider2D other) // Função chamada para quando o jogador faz ponto ao passar pelos canos, ou morre ao colidir com os canos
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.gameObject.tag == "Score")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }


}

