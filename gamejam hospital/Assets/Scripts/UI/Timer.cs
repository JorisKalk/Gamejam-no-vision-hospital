using TMPro;
using Unity.VisualScripting;
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
    [SerializeField]
    private PlayerMovement player;

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
        else if (!gameEnded)
        {
            gameMusic.Stop();
            sleepMusic.Play();
            gameEnded = true;
            player.EndGame();
            //add trigger for player sleep animation here,
            //then add trigger at end of animation to go back to menu.
            //also make sure to lock controls after that.
        }
        int displayedTime = (int)timeLeft;
        timerText.text = "Time left: " + displayedTime;
    }
}
