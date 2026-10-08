using UnityEngine;
using TMPro;

public class GoalZone : MonoBehaviour
{
    [Header("Configuración de Detección")]
    [SerializeField] private string targetTag = "Chicken"; //tag del objeto que debe ser entregado en la meta

    [Header("UI de Victoria")]
    [SerializeField] private GameObject victoryPanel;      
    [SerializeField] private TextMeshProUGUI victoryText;   //texto de victoria en pantalla

    [Header("UI de Entrega")]
    [SerializeField] private TextMeshProUGUI deliverPromptText; //texto de "[ E ] Presiona para entregar gallina"

    [Header("Señal Visual")]
    [SerializeField] private Renderer zoneRenderer;      //Renderer de la zona para cambiar color
    [SerializeField] private Color victoryColor = Color.green;
    
    //variable para controlar si la meta ya fue completada
    private bool isCompleted = false;

    private void Start()
    {
        //inicializar la meta como no completada y ocultar los elementos de UI
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
         //ocultar el texto de entrega al inicio
        if (deliverPromptText != null)
        {
            deliverPromptText.gameObject.SetActive(false);
        }
    }
//función que se llama cuando un objeto entra en la zona de meta
    private void OnTriggerStay(Collider other)
    {
        if (isCompleted) return;

        //comprobamos si lo que está adentro es la gallina o el jugador llevándola
        bool esLaGallina = other.CompareTag(targetTag) || 
                           other.transform.root.CompareTag(targetTag);

        bool esElJugador = other.CompareTag("Player") || 
                           other.transform.root.CompareTag("Player");

        if (esLaGallina || esElJugador)
        {
            //busca si la gallina está en la escena y si sigue emparentada al perro
            GameObject chicken = GameObject.FindWithTag(targetTag);

            if (chicken != null)
            {
                //si la gallina NO tiene padre, significa que ya fue soltada dentro de la meta
                if (chicken.transform.parent == null)
                {
                    if (deliverPromptText != null) deliverPromptText.gameObject.SetActive(false);
                    TriggerVictory();
                }
                else
                {
                    //si sigue agarrada en la boca, se muestra el aviso para que la suelte con E
                    if (deliverPromptText != null)
                    {
                        deliverPromptText.gameObject.SetActive(true);
                        deliverPromptText.text = "[ E ] Presiona para entregar la gallina";
                    }
                }
            }
        }
    }
//función que se llama cuando un objeto sale de la zona de meta
    private void OnTriggerExit(Collider other)
    {
        //si el jugador se va de la meta sin soltarla, se oculta el aviso
        if (deliverPromptText != null && !isCompleted)
        {
            deliverPromptText.gameObject.SetActive(false);
        }
    }

    //función que maneja la victoria al entregar el objeto en la meta
    private void TriggerVictory()
    {
        isCompleted = true;

        //se mostrara el panel de victoria y el mensaje en pantalla
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        if (victoryText != null)
        {
            victoryText.text = "¡NIVEL COMPLETADO!\nGallina rescatada con éxito";
        }

        //cambiara el color de la plataforma de meta
        if (zoneRenderer != null)
        {
            zoneRenderer.material.color = victoryColor;
            //soporte para shaders URP Lit:
            if (zoneRenderer.material.HasProperty("_BaseColor"))
            {
                zoneRenderer.material.SetColor("_BaseColor", victoryColor);
            }
        }
       Debug.Log("<color=green><b>¡VICTORIA CONFIRMADA! La gallina fue depositada en la meta.</b></color>");
    }
}
