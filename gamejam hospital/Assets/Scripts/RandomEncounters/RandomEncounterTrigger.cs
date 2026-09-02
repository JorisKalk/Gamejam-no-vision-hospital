using UnityEngine;

public class RandomEncounterTrigger : MonoBehaviour
{
    [SerializeField]
    private SpawningRandomEncounter encounterSpawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            encounterSpawner.SpawnObject();
        }
    }
}
