using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timeLimit = 300f;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private AudioSource sleepMusic;
    [SerializeField]
    private AudioSource gameMusic;

    private float timeLeft;
    private bool gameEnded = false;

    private void Start()
    {
        timeLeft = timeLimit;
    }

    private void Update()
    {
        if (!gameEnded && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            sleepMusic.Play();
            gameMusic.Stop();
            gameEnded = true;
            //add trigger for player sleep animation here,
            //then add trigger at end of animation to go back to menu.
            //also make sure to lock controls after that.
        }
        int displayedTime = (int)timeLeft;
        timerText.text = "Time left: " + displayedTime;
    }
}
