using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyData))]
public class EnemyDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyData data = (EnemyData)target;

        GUILayout.Label("Enemy Configuration", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUI.BeginChangeCheck();

        // Enemy Name
        data.enemyName = EditorGUILayout.TextField("Enemy Name", data.enemyName);
        if (string.IsNullOrEmpty(data.enemyName))
            EditorGUILayout.HelpBox("Missing enemy name!", MessageType.Error);

        // Prefab
        data.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", data.prefab, typeof(GameObject), false);
        if (data.prefab == null)
            EditorGUILayout.HelpBox("Missing prefab!", MessageType.Error);

        // Health
        data.health = EditorGUILayout.IntSlider("Health", data.health, 1, 10);

        // Speed
        data.speed = EditorGUILayout.FloatField("Speed", data.speed);

        // Rotation
        data.shouldRotate = EditorGUILayout.Toggle("Should Rotate", data.shouldRotate);
        if (data.shouldRotate)
            data.rotationSpeed = EditorGUILayout.FloatField("Rotation Speed", data.rotationSpeed);

        // Death Effect
        data.deathEffect = (GameObject)EditorGUILayout.ObjectField("Death Effect", data.deathEffect, typeof(GameObject), false);

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(data);

        EditorGUILayout.Space();

        //  Spawn Test Button
        GUI.enabled = !Application.isPlaying && data.prefab != null;
        if (GUILayout.Button("▶️ Spawn In Scene"))
        {
            SpawnEnemyPreview(data);
        }
        GUI.enabled = true;
    }

    void SpawnEnemyPreview(EnemyData data)
    {
        GameObject instance = PrefabUtility.InstantiatePrefab(data.prefab) as GameObject;
        if (instance == null)
        {
            Debug.LogError("Enemy prefab could not be instantiated.");
            return;
        }

        instance.name = $"[Preview] {data.enemyName}";
        instance.transform.position = Vector3.zero;

        Enemy enemy = instance.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.health = data.health;
            enemy.speed = data.speed;
            enemy.shouldRotate = data.shouldRotate;
            enemy.rotationSpeed = data.rotationSpeed;
            enemy.deathEffectPrefab = data.deathEffect;
        }
        else
        {
            Debug.LogWarning("Spawned prefab has no Enemy script attached.");
        }

        Undo.RegisterCreatedObjectUndo(instance, "Spawn Enemy Preview");
        Selection.activeGameObject = instance;
    }
}
