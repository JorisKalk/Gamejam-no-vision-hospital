using System.Collections;
using UnityEngine;
using TMPro;

public class VoiceOverSubtitle : MonoBehaviour
{
    [System.Serializable]
    public class SubtitleLine
    {
        [TextArea(6, 4)]
        public string text;

        public float startTime;
        public float duration;
    }

    [Header("UI")]
    public TextMeshProUGUI subtitleText;

    [Header("Text Area")]
    [Tooltip("Distance from the left and right edges of the screen.")]
    public float horizontalMargin = 100f;

    [Tooltip("Distance from the top and bottom of the screen.")]
    public float verticalMargin = 100f;

    [Header("Fade Settings")]
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;

    [Header("Subtitles")]
    public SubtitleLine[] subtitles;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        SetupTextArea();

        canvasGroup = subtitleText.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = subtitleText.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        subtitleText.text = "";

        StartCoroutine(PlaySubtitleSequence());
    }

    private void SetupTextArea()

    {
        RectTransform rect = subtitleText.rectTransform;

        // Stretch across the screen while keeping margins
        rect.anchorMin = new Vector2(0f, 0.5f);
        rect.anchorMax = new Vector2(1f, 0.5f);

        rect.pivot = new Vector2(0.5f, 0.5f);

        // Set the boundaries of the text area
        rect.offsetMin = new Vector2(
            horizontalMargin,
            -verticalMargin
        );

        rect.offsetMax = new Vector2(
            -horizontalMargin,
            verticalMargin
        );

        // Center the text horizontally and vertically
        subtitleText.alignment = TextAlignmentOptions.Center;

        // Enable normal word wrapping
        subtitleText.textWrappingMode = TextWrappingModes.Normal;

        // Prevent text from rendering outside the text box
        subtitleText.overflowMode = TextOverflowModes.Truncate;

        // Don't automatically resize the font
        subtitleText.enableAutoSizing = false;
    }


    private IEnumerator PlaySubtitleSequence()
    {
        float currentTime = 0f;

        foreach (SubtitleLine line in subtitles)
        {
            float waitTime = line.startTime - currentTime;

            if (waitTime > 0f)
                yield return new WaitForSeconds(waitTime);

            currentTime = line.startTime;

            subtitleText.text = line.text;

            // Fade in
            yield return StartCoroutine(
                Fade(0f, 1f, fadeInDuration)
            );

            // Stay visible
            yield return new WaitForSeconds(line.duration);

            // Fade out
            yield return StartCoroutine(
                Fade(1f, 0f, fadeOutDuration)
            );

            currentTime += line.duration + fadeOutDuration;
        }

        subtitleText.text = "";
    }

    private IEnumerator Fade(
        float startAlpha,
        float endAlpha,
        float duration
    )
    {
        if (duration <= 0f)
        {
            canvasGroup.alpha = endAlpha;
            yield break;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            canvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                endAlpha,
                t
            );

            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}





