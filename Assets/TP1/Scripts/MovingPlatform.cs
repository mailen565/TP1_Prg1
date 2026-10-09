using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Puntos de Trayectoria")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float waitTime = 2.0f;

    private Vector3 currentTarget;
    private bool isWaiting = false;
    private bool goingToB = true;

    private void Start()
    {
        Vector3 posA = (pointA != null) ? pointA.position : transform.position;
        Vector3 posB = (pointB != null) ? pointB.position : transform.position + new Vector3(5f, 0f, 0f);
        currentTarget = posB;
    }

    private void FixedUpdate()
    {
        if (isWaiting || pointA == null || pointB == null) return;

        // Movemos en FixedUpdate para estar en sincronía con la física del perro
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, currentTarget) < 0.05f)
        {
            isWaiting = true;
            Invoke(nameof(SwitchDirection), waitTime);
        }
    }

    private void SwitchDirection()
    {
        goingToB = !goingToB;
        currentTarget = goingToB ? pointB.position : pointA.position;
        isWaiting = false;
    }

    // Funciona con colisión sólida
    private void OnCollisionEnter(Collision collision)
    {
        EvaluarPasajero(collision.gameObject, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        EvaluarPasajero(collision.gameObject, false);
    }

    // Funciona si el collider está en modo Trigger
    private void OnTriggerEnter(Collider other)
    {
        EvaluarPasajero(other.gameObject, true);
    }

    private void OnTriggerStay(Collider other)
    {
        // Respaldo continuo: si el perro está encima, asegurar que sea hijo
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            if (other.transform.root.parent != transform)
            {
                other.transform.root.SetParent(transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EvaluarPasajero(other.gameObject, false);
    }

    private void EvaluarPasajero(GameObject obj, bool entrar)
    {
        if (obj.CompareTag("Player") || obj.transform.root.CompareTag("Player"))
        {
            if (entrar)
            {
                obj.transform.root.SetParent(transform);
            }
            else
            {
                // Solo desemparentar si actualmente era hijo de esta plataforma
                if (obj.transform.root.parent == transform)
                {
                    obj.transform.root.SetParent(null);
                }
            }
        }
    }
}