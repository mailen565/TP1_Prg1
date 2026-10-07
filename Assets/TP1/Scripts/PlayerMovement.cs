using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Configuración de Movimiento")] 
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 720.0f;
    [Header("Referencias")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    private Vector3 movementInput;
    private void Start()
    {
        //obtiene referencias de los componentes Rigidbody y Animator si no se han asignado en el Inspector
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    //Update is called once per frame
    private void Update()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");
        movementInput = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (anim != null)
        {
            anim.SetBool("isWalking", movementInput.magnitude > 0.1f);
        }
    }
    private void FixedUpdate()
    {
        if (movementInput.magnitude > 0.1f)
        {
            //Movimiento del jugador
            Vector3 movement = movementInput * speed;
            rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

            //Rotación del jugador hacia la dirección del movimiento
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else 
        {
           rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }
}

