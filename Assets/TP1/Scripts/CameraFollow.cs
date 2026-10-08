using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Transform target;

    [Header("Distancia y Ángulo")]
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float height = 2.5f;
    [SerializeField] private float smoothSpeed = 8.0f;

    private void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada detrás del jugador
        Vector3 targetBack = -target.forward * distance;
        Vector3 desiredPosition = target.position + targetBack + Vector3.up * height;

        // Transición suave de posición
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Apuntar hacia el jugador
        Vector3 lookTarget = target.position + Vector3.up * 1.0f;
        if (lookTarget - transform.position != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), smoothSpeed * Time.deltaTime);
        }
    }
}