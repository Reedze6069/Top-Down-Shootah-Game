using UnityEngine;
using UnityEngine.UIElements;

public class GameHUD : MonoBehaviour
{
    private Label healthLabel;
    private Label timerLabel;
    private PlayerController player;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Grab the labels by their assigned names
        healthLabel = root.Q<Label>("health-label");
        timerLabel = root.Q<Label>("timer-label");

        player = Object.FindFirstObjectByType<PlayerController>();
    }

    void Update()
    {
        if (player != null && healthLabel != null)
            healthLabel.text = "HP " + player.health;

        if (timerLabel != null)
        {
            float time = Time.timeSinceLevelLoad;
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerLabel.text = $"{minutes:00}:{seconds:00}";
        }
    }
}