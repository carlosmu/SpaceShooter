using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard; // Daño
    public Vector3 spawnValues; // Posición de Instanciación de asteroides
    public int hazardCount; // Cantidad de asteroides
    public float spawnWait; // Tiempo de espera de instanciación de asteroides (por cada uno de ellos)
    public float startWait; // Tiempo de espera inicial para instanciar asteroides
    public float waveWait; // Tiempo de espera de instanciación de asteroides (por olas)

    // Variables para la puntuación
    private int score;
    public Text scoreText;

    //Variables para gameOver y restart
    public Text restartText;
    public Text gameOverText;
    
    private bool restart;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        // Restart en false y ocultar su texto de nueva partida
        restart = false;
        restartText.gameObject.SetActive(false);
        // Game Over en false y ocultar su texto
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        // Poner el Score en 0 y Actualizar score
        score = 0;
        UpdateScore();
        // Corrutina de Instanciar Olas de Asteroides
        StartCoroutine(SpawnWaves());        
    }

    void Update()
    {
        if(restart && Input.GetKeyDown(KeyCode.R))
        {
            // Recarga la escena en base a la escena activa.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

    // Instanciar holas de asterioide mientras las condiciones se cumplan
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            // Generar olas de asteroides
            for (int i=0; i<hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            // Si hemos perdido, mostrar el texto para una nueva partida
            if (gameOver)
            {
                restartText.gameObject.SetActive(true);
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }

    // Actualizar el score
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    // Game over
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
    }

}
