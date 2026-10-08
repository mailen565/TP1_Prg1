using System.Collections;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    [Header("Efecto")]
    [SerializeField] private float speedMultiplier = 1.8f; //multiplicador de velocidad
    [SerializeField] private float effectDuration = 6f; //duración del efecto de aumento de velocidad

    [Header("Recarga (Cooldown)")]
    [SerializeField] private float cooldownTime = 4f; //tiempo de recarga antes de poder usarlo nuevamente

    [Header("Componentes Visuales")]
    [SerializeField] private Renderer itemRenderer; //Renderer del objeto para ocultarlo durante la recarga
    [SerializeField] private Collider itemCollider; //collider del objeto para desactivarlo durante la recarga

    private bool isAvailable = true;

//función que se llama cuando un objeto entra en el trigger del Power-Up
    private void OnTriggerEnter(Collider other)
    {
        if (!isAvailable) return;

        // Comprobación por Tag estricta
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
            if (player != null)
            {
                player.ApplySpeedBoost(speedMultiplier, effectDuration);
                StartCoroutine(CooldownRoutine());
            }
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isAvailable = false;

        // Regla de clase: NO usar SetActive(false) en el GameObject completo
        // Se oculta la parte visual y se apaga el collider
        if (itemRenderer != null) itemRenderer.enabled = false;
        if (itemCollider != null) itemCollider.enabled = false;

        yield return new WaitForSeconds(cooldownTime);

        // Se restablece para volver a usarse
        if (itemRenderer != null) itemRenderer.enabled = true;
        if (itemCollider != null) itemCollider.enabled = true;

        isAvailable = true;
    }
}