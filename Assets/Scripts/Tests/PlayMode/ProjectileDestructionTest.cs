using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ProjectileDestructionTest
{
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("SampleScene");
        yield return null;
    }

    [UnityTest]
    public IEnumerator ProjectileDestroysItself_After2Seconds()
    {
        yield return null;

        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        Assert.IsNotNull(player, "PlayerController not found");

        GameObject projectileObj = Object.Instantiate(player.projectile, player.shotPoint.position, player.shotPoint.rotation);
        Assert.IsNotNull(projectileObj, "Projectile not instantiated");

        // Wait longer than the 2s destroy delay
        yield return new WaitForSeconds(2.1f);

        bool wasDestroyed = projectileObj == null || projectileObj.Equals(null);
        Assert.IsTrue(wasDestroyed, "Projectile was not destroyed after 2 seconds");
    }

}