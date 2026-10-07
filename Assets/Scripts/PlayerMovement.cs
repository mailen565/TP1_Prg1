using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Configuración de Movimiento")] 
    [SerializeField] private float speed = 5.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); 
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
        }
        Vector3 displacement = direction * speed * Time.deltaTime;
        transform.Translate(displacement, Space.World);
    }
}

