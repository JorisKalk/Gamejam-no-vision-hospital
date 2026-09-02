using UnityEngine;
using TMPro;

public class DelayedActivation : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI textMeshPro;

    public float audioDelay = 3f;
    public float textDelay = 5f;

    void Start()
    {
        audioSource.gameObject.SetActive(false);
        textMeshPro.gameObject.SetActive(false);

        Invoke(nameof(ActivateAudio), audioDelay);
        Invoke(nameof(ActivateText), textDelay);
    }

    void ActivateAudio()
    {
        audioSource.gameObject.SetActive(true);
        audioSource.Play();
    }

    void ActivateText()
    {
        textMeshPro.gameObject.SetActive(true);
    }
}