using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;

    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
         
        /* 
         * Misma funcionalidad, código reducido:
         * gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
         */
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) return;

        // Cuando el player es destruido y se disparan la explosión y el "GameOver"
        
        // Si explosion (en el inspector) es distinto de null...
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        
        // Si impacta con objetos que tienen el tag "Player"...
        if(other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        // Agregar puntaje
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
