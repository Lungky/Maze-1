using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float stopwatch;
    public float speed = 1;
    bool timerActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true)
        {
            stopwatch += Time.deltaTime * speed; // Time.deltaTime is the time in seconds it took to complete the last frame
            string hours = Mathf.Floor((stopwatch % 216000) / 3600).ToString("00");
            string minutes = Mathf.Floor((stopwatch % 3600) / 60).ToString("00");
            string seconds = (stopwatch % 60).ToString("00");
            timerText.text = hours + ":" + minutes + ":" + seconds;
        }
    }

    public void TimerStart()
    {
        timerActive = true;
    }

    public void TimerStop()
    {
        timerActive = false;
    }
}
