using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialZone : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [TextArea(2, 4)]
    [SerializeField] private string message = "¡Hongos mágicos! Aumentan tu velocidad temporalmente y están repartidos por el mapa.";
    [SerializeField] private float displayDuration = 5f;

    private bool alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            alreadyTriggered = true; //se asegura de que nunca más vuelva a saltar
            StartCoroutine(ShowTutorialRoutine());
        }
    }

    private IEnumerator ShowTutorialRoutine()
    {
        if (tutorialText != null)
        {
            tutorialText.text = message;
            tutorialText.gameObject.SetActive(true);

            yield return new WaitForSeconds(displayDuration);

            tutorialText.gameObject.SetActive(false);
        }

        //destruye el sensor de la zona para liberar memoria
        Destroy(gameObject);
    }
}