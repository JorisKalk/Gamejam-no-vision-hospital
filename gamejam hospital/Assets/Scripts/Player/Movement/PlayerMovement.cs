using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 10f;

    [Header("Cooldown for teleporting")]
    [SerializeField]
    private float teleportCooldown = .5f;
    private float teleportCooldownLeft;
    private bool canTeleport = true;

    private Vector2 movementDir = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (teleportCooldownLeft > 0)
        {
            teleportCooldownLeft -= Time.deltaTime;
            canTeleport = false;
        }
        else
        {
            canTeleport = true;
        }

        movementDir = new Vector2(0, 0);
        Movement();
        if (movementDir != Vector2.zero)
        {
            RotatePlayer();
        } 
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W)) movementDir.y += 1;
        if (Input.GetKey(KeyCode.S)) movementDir.y -= 1;
        if (Input.GetKey(KeyCode.A)) movementDir.x -= 1;
        if (Input.GetKey(KeyCode.D)) movementDir.x += 1;

        rb.linearVelocity = movementDir.normalized * moveSpeed;
    }

    private void RotatePlayer()
    {
        if (movementDir == new Vector2(0, -1)) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (movementDir == new Vector2(1, -1)) transform.rotation = Quaternion.Euler(0, 0, 45);
        else if (movementDir == new Vector2(1, 0)) transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (movementDir == new Vector2(1, 1)) transform.rotation = Quaternion.Euler(0, 0, 135);
        else if (movementDir == new Vector2(0, 1)) transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (movementDir == new Vector2(-1, 1)) transform.rotation = Quaternion.Euler(0, 0, 225);
        else if (movementDir == new Vector2(-1, 0)) transform.rotation = Quaternion.Euler(0, 0, 270);
        else if (movementDir == new Vector2(-1, -1)) transform.rotation = Quaternion.Euler(0, 0, 315);
    }

    public bool CanTeleport()
    {
        return canTeleport;
    }

    public void TeleportPlayer(Transform targetPos)
    {
        transform.position = targetPos.position;
        teleportCooldownLeft = teleportCooldown;
        canTeleport = false;
    }
}
