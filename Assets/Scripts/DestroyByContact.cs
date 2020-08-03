using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;

    public int scoreValue;
    public GameController gameController;

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
        if (other.CompareTag("Boundary")) return;

        // Cuando el player es destruido y se disparan la explosión y el "GameOver"
        Instantiate(explosion, transform.position, transform.rotation);
        if(other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
