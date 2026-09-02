using Unity.VisualScripting;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject teleportTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                if (player.CanTeleport())
                {
                    player.TeleportPlayer(teleportTarget.transform);
                }
            }
        }
    }
}
