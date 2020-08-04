using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards; // Asteroide    
    public Vector3 spawnValues; // Posición de Instanciación de asteroides
    public int hazardCount; // Cantidad de asteroides
    public float spawnWait; // Tiempo de espera de instanciación de asteroides (por cada uno de ellos)
    public float startWait; // Tiempo de espera inicial para instanciar asteroides
    public float waveWait; // Tiempo de espera de instanciación de asteroides (por olas)

    // Variables para la puntuación
    private int score;
    public Text scoreText;

    //Variables para gameOver y restart
    public GameObject restartGameObject;
    public GameObject gameOverGameObject;
    
    private bool restart;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSpawnValues();
        // Restart en false y ocultar su texto de nueva partida
        restart = false;
        restartGameObject.SetActive(false);
        // Game Over en false y ocultar su texto
        gameOver = false;
        gameOverGameObject.SetActive(false);
        // Poner el Score en 0 y Actualizar score
        score = 0;
        UpdateScore();
        // Corrutina de Instanciar Olas de Asteroides
        StartCoroutine(SpawnWaves());        
    }

    void UpdateSpawnValues()
    {
        // Calcula el espacio para spawn dinámicamente en base al ratio del dispositivo
        Vector2 half = Utils.GetHalfDimensionsInWorldUnits();
        spawnValues = new Vector3(half.x - 0.7f, 0f, half.y + 6f);
    }

    void Update()
    {
        if(restart && Input.GetKeyDown(KeyCode.R))
        {
            // Recarga la escena en base a la escena activa.
            Restart();             
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
                // Crea la variable para el asteroide unico. Selecciona aleatoriamente entre 0 y el total de elementos.
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
                // Instancia en la posición indicada en spawn values
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            // Si hemos perdido, mostrar el texto para una nueva partida
            if (gameOver)
            {
                restartGameObject.SetActive(true);
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
        gameOverGameObject.SetActive(true);
        gameOver = true;
    }

}
