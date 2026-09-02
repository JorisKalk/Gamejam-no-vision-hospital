using System.Collections;
using UnityEngine;
using TMPro;

public class VoiceOverSubtitle : MonoBehaviour
{
    [System.Serializable]
    public class SubtitleLine
    {
        [TextArea(2, 4)]
        public string text;

        // Time in seconds before this subtitle appears
        public float startTime;

        // How long the subtitle stays visible
        public float duration;
    }

    [Header("UI")]
    public TextMeshProUGUI subtitleText;

    [Header("Layout")]
    public float horizontalMargin = 100f;

    [Header("Fade Settings")]
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;

    [Header("Subtitles")]
    public SubtitleLine[] subtitles;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        // Make sure the text is centered on screen
        RectTransform rect = subtitleText.rectTransform;

        rect.anchorMin = new Vector2(0f, 0.5f);
        rect.anchorMax = new Vector2(1f, 0.5f);

        rect.pivot = new Vector2(0.5f, 0.5f);

        rect.offsetMin = new Vector2(horizontalMargin, rect.offsetMin.y);
        rect.offsetMax = new Vector2(-horizontalMargin, rect.offsetMax.y);

        // Center the text horizontally and vertically
        subtitleText.alignment = TextAlignmentOptions.Center;

        // Allow the text to expand/wrap horizontally within the screen
        subtitleText.enableWordWrapping = true;
        subtitleText.overflowMode = TextOverflowModes.Overflow;

        // Get or create CanvasGroup for fading
        canvasGroup = subtitleText.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = subtitleText.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        subtitleText.text = "";

        // Start automatically
        StartCoroutine(PlaySubtitleSequence());
    }

    private IEnumerator PlaySubtitleSequence()
    {
        float currentTime = 0f;

        foreach (SubtitleLine line in subtitles)
        {
            // Wait until the subtitle's start time
            float waitTime = line.startTime - currentTime;

            if (waitTime > 0)
                yield return new WaitForSeconds(waitTime);

            currentTime = line.startTime;

            // Set subtitle text
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

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
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




