using UnityEditor;
using UnityEngine;

public static class GameCreationOptions
{
    private const string playerPrefabPath = "Assets/Prefabs/PlayerPrefab.prefab";

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
            // Position in scene (e.g. center of view)
            playerInstance.transform.position = Vector3.zero;
            Undo.RegisterCreatedObjectUndo(playerInstance, "Create Player");
            Selection.activeGameObject = playerInstance;
        }
    }
}


//Editor window for the spawn timer & for the background colors

// Scriptable objects to create enemies 

//Right click menu that creates game presets (the above)

//gizmos for spawn points