using UnityEngine;

public class PlayBabySound : MonoBehaviour
{
    [SerializeField]
    private AudioSource babySounds;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            babySounds.Play();
        }
    }
}
