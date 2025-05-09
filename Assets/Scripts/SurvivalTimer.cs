using UnityEngine;
using TMPro;

public class SurvivalTimer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        if (timerText != null)
            timerText.text = $" {minutes:00}:{seconds:00}";
    }
}