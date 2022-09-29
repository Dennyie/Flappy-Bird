using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private int score;
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
        gameOver.SetActive(false);
        scoreboard.SetActive(false);
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

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        ScoreText.text = score.ToString();
    }

   
}
