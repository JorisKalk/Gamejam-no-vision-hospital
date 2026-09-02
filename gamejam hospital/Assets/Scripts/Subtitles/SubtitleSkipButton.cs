using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SubtitleSkipButton : MonoBehaviour
{
    [Header("References")]
    public VoiceOverSubtitle subtitleSystem;
    public Button skipButton;

    [Header("Settings")]
    public KeyCode skipKey = KeyCode.Space;
    public float fadeDuration = 0.3f;

    private CanvasGroup canvasGroup;
    private bool isVisible = false;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // Hide the button at the start
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Connect the button click
        skipButton.onClick.AddListener(Skip);
    }

    private void Update()
    {
        // Press Space to reveal the skip button
        if (Input.GetKeyDown(skipKey) && !isVisible)
        {
            ShowSkipButton();
        }
    }

    private void ShowSkipButton()
    {
        isVisible = true;

        StartCoroutine(FadeButton(0f, 1f));
    }

    public void Skip()
    {
        if (subtitleSystem != null)
        {
            subtitleSystem.SkipSubtitles();
        }

        StartCoroutine(FadeButton(1f, 0f));
    }

    private IEnumerator FadeButton(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        canvasGroup.interactable = endAlpha > 0f;
        canvasGroup.blocksRaycasts = endAlpha > 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / fadeDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            canvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                endAlpha,
                t
            );

            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        if (endAlpha == 0f)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}

