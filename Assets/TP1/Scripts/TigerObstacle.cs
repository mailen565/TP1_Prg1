using UnityEngine;

public class TigerObstacle : MonoBehaviour
{
    [SerializeField] private float speed = 5.5f;
    [SerializeField] private float lifeTime = 4f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //destruye el obstáculo después de un tiempo para evitar que se acumulen en la escena
        Destroy(gameObject, lifeTime);

        if (animator != null)
        {
            //activa el modo de movimiento y la animación de avance hacia adelante
            animator.SetInteger("State", 1);  //activa el modo de movimiento
            animator.SetFloat("Vert", 1f);     //velocidad de avance hacia adelante
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
}