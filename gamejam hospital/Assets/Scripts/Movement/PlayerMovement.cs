using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movementDir = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) movementDir.y += 1;
        if (Input.GetKey(KeyCode.S)) movementDir.y -= 1;
        if (Input.GetKey(KeyCode.A)) movementDir.x -= 1;
        if (Input.GetKey(KeyCode.D)) movementDir.x += 1;

        movementDir.Normalize();

        rb.linearVelocity = movementDir * moveSpeed;
    }
}
