using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float timeLimit = 300f;
    [SerializeField]
    private TextMeshProUGUI timerText;

    private float timeLeft;

    private void Start()
    {
        timeLeft = timeLimit;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        int displayedTime = (int)timeLeft;
        timerText.text = "Time left: " + displayedTime;
    }
}
