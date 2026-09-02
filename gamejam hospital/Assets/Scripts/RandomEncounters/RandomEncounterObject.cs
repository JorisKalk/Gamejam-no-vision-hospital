using UnityEngine;

public class RandomEncounterObject : MonoBehaviour
{
    private Rigidbody2D rb;

    private float timeToExpire;
    private float moveSpeed;
    private Vector2 movementDir = Vector2.zero;

    private bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isMoving)
        {
            if (timeToExpire > 0)
            {
                timeToExpire -= Time.deltaTime;
                rb.linearVelocity = movementDir * moveSpeed;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void StartMoving(float pTimeToExpire, float pMoveSpeed, Vector2 pMovementDir)
    {
        timeToExpire = pTimeToExpire;
        moveSpeed = pMoveSpeed;
        movementDir = pMovementDir;
        isMoving = true;
    }
}
