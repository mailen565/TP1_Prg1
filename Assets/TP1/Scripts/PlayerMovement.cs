using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 12.0f;

    [Header("Referencias")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform model; // El hijo Dog_001
    [SerializeField] private Camera gameCamera; // Tu cámara activa

    private Vector3 movementInput;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (anim == null) anim = GetComponentInChildren<Animator>();
        if (model == null && anim != null) model = anim.transform;
        if (gameCamera == null) gameCamera = Camera.main;

        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 rawInput = new Vector3(h, 0f, v).normalized;

        if (rawInput.magnitude > 0.1f)
        {
            // Tomar la dirección real hacia donde mira la cámara en pantalla
            Vector3 camForward = (gameCamera != null) ? gameCamera.transform.forward : Vector3.forward;
            Vector3 camRight = (gameCamera != null) ? gameCamera.transform.right : Vector3.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            // Ahora 'W' siempre irá hacia el fondo de la pantalla y 'D' a la derecha
            movementInput = (camForward * rawInput.z + camRight * rawInput.x).normalized;

            // Rotar suavemente hacia la dirección a la que se mueve
            Quaternion targetRotation = Quaternion.LookRotation(movementInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

           if (anim != null) 
           {
            float moveSpeedParam = (movementInput.magnitude > 0.1f) ? 1.0f : 0.0f;
            anim.SetFloat("Vert", moveSpeedParam);
            }
        }
        else
        {
            movementInput = Vector3.zero;
            if (anim != null) 
            {
                anim.SetFloat("Vert", 0.0f);
            }
        }
    }
    private void FixedUpdate()
    {
        if (rb == null) return;

        if (movementInput.magnitude > 0.1f)
        {
            Vector3 targetVelocity = movementInput * speed;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        }
        else
        {
            if(transform.parent != null)
            {
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            }
            else
            {
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            }
        }
    }
}