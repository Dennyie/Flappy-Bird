using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    // SerializeField para conseguir editar os valores dessas váriaveis no Inspector
    [SerializeField] private float jumpforce;
    [SerializeField] private float timeup;
    [SerializeField] private Transform flappydown;
    [SerializeField] private float timecount;

    [SerializeField] private Rigidbody2D rigidbody2d;
    private float maxRotation = 25f;
    private float minRotation = -90f;

    // OnValidate para usar o GetComponent
#if UNITY_EDITOR    // A utilização do unity_editor é para que não haja problemas em build

    void OnValidate()   //// Onvalidate só roda em editor
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }


#endif

    private void Awake()
    {
        GameManager.instance.myDelegate += OnGameStart;
    }

    private void OnGameStart()
    {
        this.enabled = true;  // Ativa o "flappy" após o play 

        transform.position = new Vector3(0, 0, 0);       // Para setar a posição do flappy em 0, 0, 0 toda vez que o jogador apertar play
        transform.rotation = new Quaternion(0, 0, 0, 0); // Para setar a rotação do flappy em 0, 0, 0 toda vez que o jogador apertar play
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;             // Deixando o flappy em modo Kinematic, queria que toda vez que o jogador iniciasse o jogo o flappy começasse parado e não que começasse a cair imediatamente
        rigidbody2d.Sleep();
    }


    void Update() // Update é chamado 1 vez por frame 
    {

        //  Debug.Log("velocity " + rigidbody2d.velocity.y);    // Debug log para ver a velocidade que o flappy está caindo
        if (IsPressingJump())
        {
            rigidbody2d.bodyType = RigidbodyType2D.Dynamic; //  Apertar espaço faz com que o flappy deixe de ser Kinematic e volte a ser Dynamic, sendo afetado pela gravidade e voltando a poder cair normalmente 
            Jumpflappy();        // Chama a função Jumpflappy
            RotateFlappy();     // Chama a função RotateFlappy
        }
        if (IsFalling())    // Se a velocidade Y do rigidbody2d for menor que -7 ele executa a função abaixo
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, flappydown.rotation, timecount * Time.deltaTime);   // A função quaternion.slerp faz o flappy iniciar uma rotação 
        }
        LimitRotation();    // Função para limitar a rotação do flappy
    }

    private bool IsFalling()
    {
        return rigidbody2d.velocity.y < -7;
    }

    private static bool IsPressingJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void Jumpflappy()   // Faz o flappy pular
    {
        rigidbody2d.velocity = Vector2.up * jumpforce;   // Muda a velocidade do rigidbody2d para fazer o flappy pular
    }

    // configura a rotação do flappy para 30 
    private void RotateFlappy() // Função para fazer o flappy ficar com bico levantado toda vez que apertamos espaço
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 25);   
    }

    private void LimitRotation()    // Clamp para não deixar o flappy rotacionar mais do que o normal
    {
        Vector3 playerEulerAngles = transform.rotation.eulerAngles;

        playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, minRotation, maxRotation);

        transform.rotation = Quaternion.Euler(playerEulerAngles);

    }

    private void OnTriggerEnter2D(Collider2D other) // Função chamada para quando o jogador faz ponto ao passar pelos canos, ou morre ao colidir com os canos
    {
        if (other.gameObject.tag == "Obstacle")
        {
            GameManager.instance.GameOver();
        } else if (other.gameObject.tag == "Score")
        {
            GameManager.instance.IncreaseScore();
        }
    }


}

