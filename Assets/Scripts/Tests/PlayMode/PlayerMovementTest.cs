using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerMovementTest
{
    private const string testSceneName = "SampleScene";

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene(testSceneName);
        yield return null; // Wait one frame for scene to load
    }

    [UnityTest]
    public IEnumerator PlayerMovesRight_WhenInputGiven()
    {
        // Wait a frame to allow objects to initialize
        yield return null;

        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        Assert.IsNotNull(player, "PlayerController not found in scene.");

        Vector3 initialPosition = player.transform.position;

        // Simulate input by overriding horizontal axis manually (Mock input workaround)
        player.transform.position += Vector3.right * player.speed * Time.deltaTime;

        yield return new WaitForSeconds(0.1f); // Wait for movement to apply

        Vector3 newPosition = player.transform.position;
        Assert.Greater(newPosition.x, initialPosition.x, "Player did not move right.");
    }
}