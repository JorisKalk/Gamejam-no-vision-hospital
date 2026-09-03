using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DelayedActivation : MonoBehaviour
{
    [Header("References")]
    public AudioSource audioSource;
    public TextMeshProUGUI textMeshPro;

    [Header("Activation Delays")]
    public float audioDelay = 3f;
    public float textDelay = 5f;

    [Header("Scene Transition")]
    public string mainMenuScene = "MainMenu";
    public float sceneDelay = 3f;

    private void Start()
    {
        // Make sure the objects start hidden
        audioSource.gameObject.SetActive(false);
        textMeshPro.gameObject.SetActive(false);

        // Schedule activation
        Invoke(nameof(ActivateAudio), audioDelay);
        Invoke(nameof(ActivateText), textDelay);

        // Schedule scene change
        Invoke(nameof(LoadMainMenu), textDelay + sceneDelay);
    }

    private void ActivateAudio()
    {
        audioSource.gameObject.SetActive(true);
        audioSource.Play();
    }

    private void ActivateText()
    {
        textMeshPro.gameObject.SetActive(true);
    }

    private void LoadMainMenu()
    {
        if (string.IsNullOrEmpty(mainMenuScene))
        {
            Debug.LogWarning(
                "DelayedActivation: No Main Menu scene has been assigned."
            );

            return;
        }

        SceneManager.LoadScene(mainMenuScene);
    }
}
