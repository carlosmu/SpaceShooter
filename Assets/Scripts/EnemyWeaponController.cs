using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    // Retardo y tasa de disparos del enemigo.
    public float delay;
    public float fireRate;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Disparos del enemigo
        InvokeRepeating("Fire", delay, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
