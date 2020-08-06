using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public float dodge;
    public float smoothing;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;
    public float tilt;

    private float targetManeuver;
    // Referencia al rigidbody
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateBoundary();
        // Llama a la corrutina al iniciar
        StartCoroutine(Evade());
    }

    void UpdateBoundary()
    {
        // Calculamos el boundary por el cual se mueve el enemigo (código copiado del player) 
        Vector2 half = Utils.GetHalfDimensionsInWorldUnits();
        boundary.xMin = -half.x + 1.2f;
        boundary.xMax = half.x - 1.2f;
        boundary.zMin = 0;
        boundary.zMax = 0;
    }

    // Corrutina: Esperar un tiempo inicial, hacer maniobra evasiva, esperar, nueva maniobra...
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        // Debug.Log("Espera terminada");

        // IMPORTANTE!! Siempre que se haga algún bucle infinito dentro de una corrutina hay que asegurarse de que 
        // dentro del mismo se ejecutre algún yield return. Si no, el programa se quedará blqueado cuando se ejecute.
        while (true)
        {
            // Si está en la mitad izquierda o derecha, se mueve hacia la otra mitad de la pantalla
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);

            // También podríamos resolverlo con un if, es más fácil de leer, pero es más largo.
            //if (transform.position.x < 0)
            //{
            //    targetManeuver = Random.Range(1, dodge);
            //} else
            //{
            //    targetManeuver = -Random.Range(1, dodge);
            //}

            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

            targetManeuver = 0;

            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, rb.velocity.z);
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0f, rb.position.z);
        rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);

    }
}
