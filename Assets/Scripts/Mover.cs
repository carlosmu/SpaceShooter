using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1;

    private Rigidbody rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rig.velocity = transform.forward * speed;        
    }
}
