using UnityEditor;
using UnityEngine;

public static class GameCreationOptions
{
    private const string playerPrefabPath = "Assets/Prefabs/PlayerPrefab.prefab";
    private const string spawnerPrefabPath = "Assets/Prefabs/SpawnerPrefab.prefab";
    private const string breathingCirclesPrefabPath = "Assets/Prefabs/BreathingCirclesPrefab.prefab"; // ✅ NEW

    [MenuItem("GameObject/Survival Game/Create Player", false, 10)]
    public static void CreatePlayer()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(playerPrefabPath);

        if (prefab == null)
        {
            Debug.LogError($"Player prefab not found at path: {playerPrefabPath}");
            return;
        }

        GameObject playerInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (playerInstance != null)
        {
            playerInstance.transform.position = Vector3.zero;
            Undo.RegisterCreatedObjectUndo(playerInstance, "Create Player");
            Selection.activeGameObject = playerInstance;
        }
    }

    [MenuItem("GameObject/Survival Game/Create Spawner", false, 11)]
    public static void CreateSpawner()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(spawnerPrefabPath);

        if (prefab == null)
        {
            Debug.LogError($"Spawner prefab not found at path: {spawnerPrefabPath}");
            return;
        }

        GameObject spawnerInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (spawnerInstance != null)
        {
            spawnerInstance.transform.position = Vector3.zero;
            Undo.RegisterCreatedObjectUndo(spawnerInstance, "Create Spawner");
            Selection.activeGameObject = spawnerInstance;
        }
    }

    [MenuItem("GameObject/Survival Game/Create Breathing Circles", false, 12)]
    public static void CreateBreathingCircles()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(breathingCirclesPrefabPath);

        if (prefab == null)
        {
            Debug.LogError($"BreathingCircles prefab not found at path: {breathingCirclesPrefabPath}");
            return;
        }

        GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (instance != null)
        {
            instance.transform.position = Vector3.zero;
            Undo.RegisterCreatedObjectUndo(instance, "Create Breathing Circles");
            Selection.activeGameObject = instance;
        }
    }
}


//Editor window for the spawn timer & for the background colors

// Scriptable objects to create enemies 

//Right click menu that creates game presets (the above)

//gizmos for spawn points