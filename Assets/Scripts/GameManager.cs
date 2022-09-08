using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private int score;
    public Text ScoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public Jump player;
    private Transform gameObjectToMove;
    private Rigidbody2D sleep;
    [SerializeField]   private GameObject Flappy;



#if UNITY_EDITOR

    void OnValidate()
    {
        gameObjectToMove = Flappy.GetComponent<Transform>();
        sleep = Flappy.GetComponent<Rigidbody2D>();
    }


#endif

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

        Time.timeScale = 1f;
        player.enabled = true;

        PipeVelocity[] pipes = FindObjectsOfType<PipeVelocity>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        gameObjectToMove.transform.position = new Vector3(0, 0, 0);
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

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        ScoreText.text = score.ToString();
    }
}
