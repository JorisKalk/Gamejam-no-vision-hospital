using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum facingDirection
    {
        FRONT,
        BACK,
        SIDE
    }

    private Rigidbody2D rb;

    [Header("Animation References")]
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Animator anim;

    [Header("Movement Values")]
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private Transform rotationTransform;

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
        
        HandleAnimations();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W)) movementDir.y = 1;
        else if (Input.GetKey(KeyCode.S)) movementDir.y = -1;
        else movementDir.y = 0;

        if (Input.GetKey(KeyCode.A)) movementDir.x = -1;
        else if (Input.GetKey(KeyCode.D)) movementDir.x = 1;
        else movementDir.x = 0;

        rb.linearVelocity = movementDir.normalized * moveSpeed;
    }

    private void RotatePlayer()
    {
        if (movementDir == new Vector2(0, -1)) rotationTransform.rotation = Quaternion.Euler(0, 0, 0);
        else if (movementDir == new Vector2(1, -1)) rotationTransform.rotation = Quaternion.Euler(0, 0, 45);
        else if (movementDir == new Vector2(1, 0)) rotationTransform.rotation = Quaternion.Euler(0, 0, 90);
        else if (movementDir == new Vector2(1, 1)) rotationTransform.rotation = Quaternion.Euler(0, 0, 135);
        else if (movementDir == new Vector2(0, 1)) rotationTransform.rotation = Quaternion.Euler(0, 0, 180);
        else if (movementDir == new Vector2(-1, 1)) rotationTransform.rotation = Quaternion.Euler(0, 0, 225);
        else if (movementDir == new Vector2(-1, 0)) rotationTransform.rotation = Quaternion.Euler(0, 0, 270);
        else if (movementDir == new Vector2(-1, -1)) rotationTransform.rotation = Quaternion.Euler(0, 0, 315);
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


    private void HandleAnimations()
    {
        if (movementDir.x > 0) sprite.flipX = false;
        else if (movementDir.x < 0) sprite.flipX = true;

        if (movementDir.x != 0) anim.SetInteger("FacingDirection", (int)facingDirection.SIDE);
        else if (movementDir.y > 0) anim.SetInteger("FacingDirection", (int)facingDirection.BACK);
        else if (movementDir.y < 0) anim.SetInteger("FacingDirection", (int)facingDirection.FRONT);

        if (movementDir.magnitude != 0) anim.SetBool("Walking", true);
        else anim.SetBool("Walking", false);
    }
}
