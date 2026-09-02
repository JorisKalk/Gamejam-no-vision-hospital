using UnityEngine;

public class SpawningRandomEncounter : MonoBehaviour
{
    private enum direction
    {
        UP, DOWN, LEFT, RIGHT
    }

    [SerializeField]
    GameObject spawnerParent;

    [Header("Spawning Values")]
    [SerializeField]
    private GameObject objectToSpawn;
    [SerializeField]
    private float moveSpeed = 20f;
    [SerializeField]
    private float expirationTime = 3f;
    [SerializeField]
    private direction moveDir = new direction();

    private bool hasSpawned = false;

    public void SpawnObject()
    {
        if (!hasSpawned)
        {
            hasSpawned = true;
            GameObject spawnedObject = Instantiate(objectToSpawn);
            spawnedObject.transform.position = transform.position;
            RandomEncounterObject objectScript = spawnedObject.GetComponent<RandomEncounterObject>();
            if (objectScript != null)
            {
                objectScript.StartMoving(expirationTime, moveSpeed, DirectionVector());
            }
            Destroy(spawnerParent);
        }
    }

    private Vector2 DirectionVector()
    {
        switch (moveDir)
        {
            case direction.UP:
                return Vector2.up;
            case direction.DOWN:
                return Vector2.down;
            case direction.LEFT:
                return Vector2.left;
            case direction.RIGHT:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }
}
