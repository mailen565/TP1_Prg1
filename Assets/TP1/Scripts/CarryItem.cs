using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class CarryItem : MonoBehaviour
{
    //variables para configurar el comportamiento de agarre y transporte de objetos
    [Header("Configuración de Agarre")]
    [SerializeField] private float pickupRadius = 2.5f;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private TextMeshProUGUI interactionText; // Texto en pantalla

    private GameObject carriedItem = null;
    private Rigidbody carriedRb = null;
    private Collider carriedCollider = null;
    private GameObject nearbyItem = null;

    private void Update()
    {
        //detecta si hay un objeto transportable cerca
        DetectNearbyItem();

        //control del texto en pantalla
        if (interactionText != null)
        {
            if (carriedItem != null)
            {
                interactionText.gameObject.SetActive(true); //mostrar el texto
                interactionText.text = "[ E ] Soltar Gallina";
            }
            else if (nearbyItem != null)
            {
                interactionText.gameObject.SetActive(true); 
                interactionText.text = "[ E ] Agarrar Gallina";
            }
            else
            {
                interactionText.gameObject.SetActive(false); //ocultar el texto si no hay objeto cercano ni transportado
            }
        }

        //al presionar la tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (carriedItem == null && nearbyItem != null) //si no hay objeto transportado y hay un objeto cerca
            {
                PickupItem(nearbyItem); //agarrar el objeto
            }
            else if (carriedItem != null) //si hay un objeto transportado
            {
                DropItem(); //soltar el objeto
            }
        }
    }

    private void DetectNearbyItem()
    {
        //si ya hay un objeto transportado, no buscar objetos cercanos
        if (carriedItem != null)
        {
            nearbyItem = null;
            return;
        }
        //buscar objetos cercanos dentro del radio de pickup
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRadius);
        nearbyItem = null;

        //recorrer los objetos detectados y buscar uno con la etiqueta "Chicken"
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Chicken"))
            {
                nearbyItem = hit.gameObject;
                break;
            }
        }
    }
    //función para agarrar el objeto
    private void PickupItem(GameObject item)
    {
        //asignar referencias al objeto transportado
        carriedItem = item;
        carriedRb = carriedItem.GetComponent<Rigidbody>();
        carriedCollider = carriedItem.GetComponent<Collider>();

        //desactivar collider para que NO choque con el cuerpo del perro
        if (carriedCollider != null)
        {
            carriedCollider.enabled = false;
        }

        //bloquear físicas de la gallina
        if (carriedRb != null)
        {
            if (!carriedRb.isKinematic)
            {
                carriedRb.linearVelocity = Vector3.zero;
            }
            carriedRb.isKinematic = true;
            carriedRb.useGravity = false;
            
        }

        //emparentar al hocico/boca (HoldPoint)
        carriedItem.transform.SetParent(holdPoint);
        carriedItem.transform.localPosition = Vector3.zero;
        carriedItem.transform.localRotation = Quaternion.identity;
    }

    public void DropItem()
    {
        if (carriedItem == null) return;

        //desvincular de la jerarquía
        carriedItem.transform.SetParent(null);

        //reactivar el collider para que pueda colisionar con el suelo y la meta
        if (carriedCollider != null)
        {
            carriedCollider.enabled = true;
        }

        //reactivar físicas y gravedad
        if (carriedRb != null)
        {
            carriedRb.isKinematic = false; //reactivar físicas
            carriedRb.useGravity = true; //reactivar gravedad
            carriedRb.constraints = RigidbodyConstraints.None; //permite que caiga libremente
            carriedRb.linearVelocity = transform.forward * 1.5f; //pequeño impulso adelante
        }

        carriedItem = null; //limpiar referencia al objeto transportado
        carriedRb = null; //limpiar referencia al Rigidbody
        carriedCollider = null; //limpiar referencia al Collider
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}