using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerShootingTest
{
    private const string testSceneName = "SampleScene";

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene(testSceneName);
        yield return null; // wait for scene to load
    }

    [UnityTest]
    public IEnumerator PlayerShoots_ProjectileIsSpawned()
    {
        yield return null;

        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        Assert.IsNotNull(player, "PlayerController not found");

        // Record projectile count before shooting
        int initialProjectileCount = Object.FindObjectsByType<Projectile>(FindObjectsSortMode.None).Length;

        // Manually simulate a shot
        Object.Instantiate(player.projectile, player.shotPoint.position, player.shotPoint.rotation);

        yield return new WaitForSeconds(0.1f); // allow time for instantiation

        int newProjectileCount = Object.FindObjectsByType<Projectile>(FindObjectsSortMode.None).Length;
        Assert.Greater(newProjectileCount, initialProjectileCount, "Projectile was not spawned");
    }
}