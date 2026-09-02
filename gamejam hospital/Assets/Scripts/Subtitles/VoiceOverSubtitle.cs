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

    [Header("Voice Over")]
    [Tooltip("The AudioSource playing the voice-over.")]
    public AudioSource voiceOverAudio;

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
    private Coroutine subtitleCoroutine;

    private void Awake()
    {
        SetupTextArea();

        // Get or create CanvasGroup
        canvasGroup = subtitleText.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = subtitleText.gameObject.AddComponent<CanvasGroup>();
        }

        // Start hidden
        canvasGroup.alpha = 0f;
        subtitleText.text = "";

        // Automatically start subtitles
        subtitleCoroutine = StartCoroutine(PlaySubtitleSequence());
    }

    private void SetupTextArea()
    {
        RectTransform rect = subtitleText.rectTransform;

        // Stretch horizontally across the screen
        rect.anchorMin = new Vector2(0f, 0.5f);
        rect.anchorMax = new Vector2(1f, 0.5f);

        rect.pivot = new Vector2(0.5f, 0.5f);

        // Keep text away from the screen edges
        rect.offsetMin = new Vector2(
            horizontalMargin,
            -verticalMargin
        );

        rect.offsetMax = new Vector2(
            -horizontalMargin,
            verticalMargin
        );

        // Center the text
        subtitleText.alignment = TextAlignmentOptions.Center;

        // Enable word wrapping
        subtitleText.textWrappingMode = TextWrappingModes.Normal;

        // Keep text inside its boundaries
        subtitleText.overflowMode = TextOverflowModes.Truncate;

        // Keep font size fixed
        subtitleText.enableAutoSizing = false;
    }

    private IEnumerator PlaySubtitleSequence()
    {
        float currentTime = 0f;

        foreach (SubtitleLine line in subtitles)
        {
            // Wait until the subtitle's start time
            float waitTime = line.startTime - currentTime;

            if (waitTime > 0f)
            {
                yield return new WaitForSeconds(waitTime);
            }

            currentTime = line.startTime;

            // Set subtitle text
            subtitleText.text = line.text;

            // Fade in
            yield return StartCoroutine(
                Fade(0f, 1f, fadeInDuration)
            );

            // Keep subtitle visible
            yield return new WaitForSeconds(line.duration);

            // Fade out
            yield return StartCoroutine(
                Fade(1f, 0f, fadeOutDuration)
            );

            currentTime += line.duration + fadeOutDuration;
        }

        // Finished normally
        subtitleText.text = "";
        canvasGroup.alpha = 0f;

        subtitleCoroutine = null;
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

    // Called when the Skip button is pressed
    public void SkipSubtitles()
    {
        // Stop subtitle coroutine
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
            subtitleCoroutine = null;
        }

        // Stop voice-over audio
        if (voiceOverAudio != null)
        {
            voiceOverAudio.Stop();
        }

        // Hide subtitle
        subtitleText.text = "";
        canvasGroup.alpha = 0f;
    }
}
