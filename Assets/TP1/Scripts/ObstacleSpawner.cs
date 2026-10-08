using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab; //solo Prefabs
    [SerializeField] private float startDelay = 1.5f; //tiempo inicial antes de la primera salida
    [SerializeField] private float repeatRate = 3.5f;    //intervalo regular de salida

    private void Start()
    {
        //generación periódica con InvokeRepeating
        InvokeRepeating(nameof(Spawn), startDelay, repeatRate);
    }
    
    private void Spawn()
    {
        if (obstaclePrefab != null)
        {
            Instantiate(obstaclePrefab, transform.position, transform.rotation);
        }
    }
}