using UnityEngine;

public class BreathingCircle : MonoBehaviour
{
    public float scaleAmplitude = 0.1f;
    public float scaleSpeed = 1f;
    public float phaseOffset = 0f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * scaleSpeed + phaseOffset) * scaleAmplitude;
        transform.localScale = originalScale * scale;
    }
}