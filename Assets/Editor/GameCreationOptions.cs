using UnityEditor;
using UnityEngine;

public static class GameCreationOptions
{
    [MenuItem("GameObject/Survival Game/Create Player")]
    public static void CreatePlayer()
    {
        // This creates an object from Right Click > Survival Game > Create Player
        GameObject player = new GameObject("Player");
        
        // Alternatively, instantiate a copy of a prefab
    }
}

//Editor window for the spawn timer & for the background colors

// Sciptable objects to create enemies 

//Right click menu that creates game presets (the above)

//gizmos for spawn points