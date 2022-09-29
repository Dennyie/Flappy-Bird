using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private int score;
    private int highscore;
    public GameObject scorego;
    public Text scoreTextFinish;
    public Text highscoreText;
    public Text ScoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject scoreboard;
    public Jump player;
    private Transform gameObjectToMove;
    private Rigidbody2D sleep;
    [SerializeField] private GameObject Flappy;



#if UNITY_EDITOR

    void OnValidate()
    {
        gameObjectToMove = Flappy.GetComponent<Transform>();
        sleep = Flappy.GetComponent<Rigidbody2D>();
    }


#endif

    public void Start()
    {
        //PlayerPrefs.SetInt("highscore", 0);R
        gameOver.SetActive(false);
        scoreboard.SetActive(false);
        highscoreText.text = "" + PlayerPrefs.GetInt("highscore"); // O texto não aparecer 0
        highscore = PlayerPrefs.GetInt("highscore"); // declarando o highscore pra ela não começar como 0
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }

    public void Play()
    {
        score = 0;
        ScoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        scoreboard.SetActive(false);
        scorego.SetActive(true);

        Time.timeScale = 1f;
        player.enabled = true;

        PipeVelocity[] pipes = FindObjectsOfType<PipeVelocity>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        Flappy.transform.position = new Vector3(0, 0, 0);
        Flappy.transform.rotation = new Quaternion(0, 0, 0, 0);
        sleep.bodyType = RigidbodyType2D.Kinematic;
        sleep.Sleep();
        
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;

    }


    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);
        scoreboard.SetActive(true);
        scoreTextFinish.text = score.ToString();
        scorego.SetActive(false);


        Pause();

        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);  // Set int está setando o valor da variavel highscore dentro da "highscore" 
            PlayerPrefs.Save(); // Save salva o highscore
            highscoreText.text = "" + PlayerPrefs.GetInt("highscore"); // Get int está pegando o highscore do "highscore" salvo
        }
;       
    }

    public void IncreaseScore()
    {
        score++;
        ScoreText.text = score.ToString();
    }

   
}
