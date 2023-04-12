using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.UIElements;

#if Unity_editor

using UnityEditor.U2D.Path.GUIFramework;

#endif
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score;
    private int highscore;
    public GameObject scorego;
    public Text scoreTextFinish;
    public Text highscoreText;
    public Text ScoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject scoreboard;
    public Player player;
    [SerializeField] private GameObject Flappy;
    public bool isPlaying = false;
    [SerializeField] private GameObject Spawner;
    [SerializeField] private PipeSpawner spawnerscript;


#if UNITY_EDITOR    // A utilização do unity_editor é para que não haja problemas em build
    void OnValidate()   // Onvalidate só roda em editor
    {
        spawnerscript = Spawner.GetComponent<PipeSpawner>();
    }


#endif

    public delegate void MyDelegate();
    public MyDelegate playgame;

    private void Awake()    // Para ser executado antes do Start acontecer
    {
        Application.targetFrameRate = 60; // Para limitar o FPS a 60
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        
    }

    public void Start()
    {
        //PlayerPrefs.SetInt("highscore", 0);   // Comando para resetar o highscore do jogador, usado antes de buildar para o meu score não ir para build
        gameOver.SetActive(false);  // Desativa a visualização do GAMEOVER no start 
        scoreboard.SetActive(false);    // Desativa a visualização do scoreboard no start
        highscoreText.text = "" + PlayerPrefs.GetInt("highscore"); // Para que o highscore não comece sem valor, as aspas vazias é para que ele entenda que é um txt

        Pause();    // Executa a função pause

    }

    public void Play()  // Função linkada ao botão para dar play no jogo
    {
        spawnerscript.stopInvoke();
        spawnerscript.control = false;
        isPlaying = true;
        score = 0;  // Para sempre que dermos play o score começar em 0 
        ScoreText.text = score.ToString();  // Transforma variavel int em string para poder entrar como texto

        playButton.SetActive(false);    // Desativa a visualização do botão de PLAY após o play
        gameOver.SetActive(false);      // Desativa a visualização do GAMEOVER após o play
        scoreboard.SetActive(false);    // Desativa a visualização do SCOREBOARD após o play
        scorego.SetActive(true);        // Ativa a visualização do SCORE após o play

        Time.timeScale = 1f;    // Definindo a velocidade do jogo igual a velocidade de tempo real.

        playgame?.Invoke();

        PipeVelocity[] pipes = FindObjectsOfType<PipeVelocity>();

        for (int i = 0; i < pipes.Length; i++)  // Para destruir todos os canos quando o jogador apertar play, para não existir nenhum cano de uma "partida" que já acabou
        {
            Destroy(pipes[i].gameObject);
        }

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