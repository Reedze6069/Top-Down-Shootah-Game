using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SurvivalGame/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public int health = 1;
    public float speed = 2f;
    public bool shouldRotate = false;
    public float rotationSpeed = 180f;
    public GameObject deathEffect;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(enemyName))
            Debug.LogWarning($"[EnemyData] Name is missing on {name}");

        if (prefab == null)
            Debug.LogError($"[EnemyData] Prefab is missing on {name}");

        if (deathEffect == null)
            Debug.LogWarning($"[EnemyData] Death effect is not assigned on {name}");
    }
#endif
}