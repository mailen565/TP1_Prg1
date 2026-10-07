using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Puntos de Trayectoria")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float waitTime = 2.0f; // Tiempo de espera antes de cambiar de dirección

    private Vector3 currentTarget;
    private bool isWaiting = false;

    private void Start()
    {
        //inicia yendo hacia el punto B
        if (pointB != null)
        {
            currentTarget = pointB.position;
        }
    }

    private void Update()
    {
        //si está esperando mediante Invoke, no se desplaza
        if (isWaiting || pointA == null || pointB == null) return;

        //movimiento continuo hacia el destino actual
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        //comprueba si llegó a la posición objetivo
        if (Vector3.Distance(transform.position, currentTarget) < 0.05f)
        {
            isWaiting = true;
            //se utiliza Invoke para temporizar el cambio de dirección según la consigna
            Invoke(nameof(SwitchDirection), waitTime);
        }
    }

    private void SwitchDirection()
    {
        //cambia el destino entre el punto A y el punto B
        if (currentTarget == pointB.position)
        {
            currentTarget = pointA.position;
        }
        else
        {
            currentTarget = pointB.position;
        }

        //permite que vuelva a moverse
        isWaiting = false;
    }
    //asegura que el personaje no se deslice ni se caiga al viajar sobre la plataforma
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}