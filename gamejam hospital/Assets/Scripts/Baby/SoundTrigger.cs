using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private bool triggered = false;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            // Play the sound
            if (audioSource != null)
                audioSource.Play();

            // Make the sprite disappear
            if (spriteRenderer != null)
                spriteRenderer.enabled = false;
        }
    }
}
