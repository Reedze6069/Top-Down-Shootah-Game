using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform Weapon;
    public float offset;

    public Transform shotPoint;
    public GameObject projectile;
    public float timeBetweenShots;
    float nextShotTime;

    public int health = 3;
    public TMP_Text healthText;

    public float invincibilityDuration = 1.5f;
    private bool isInvincible = false;

    private Camera mainCam;
    private Vector2 spriteHalfSize;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        mainCam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Cache half of sprite size
        if (spriteRenderer != null)
            spriteHalfSize = spriteRenderer.bounds.extents;
        else
            spriteHalfSize = Vector2.zero;

        UpdateHealthUI();
    }

    void Update()
    {
        // Movement
        Vector3 playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += playerInput.normalized * (speed * Time.deltaTime);

        // Weapon Rotation
        if (Weapon != null)
        {
            Vector3 displacement = Weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
            Weapon.rotation = Quaternion.Euler(0f, 0f, angle + offset);
        }

        // Shooting
        if (Input.GetMouseButtonDown(0) && Time.time > nextShotTime)
        {
            nextShotTime = Time.time + timeBetweenShots;
            Instantiate(projectile, shotPoint.position, shotPoint.rotation);
        }

        ClampToCameraBounds();
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        health -= amount;
        UpdateHealthUI();

        if (health <= 0)
        {
            SceneManager.LoadScene("SampleScene");
            return;
        }

        StartCoroutine(InvincibilityFlash());
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "HP: " + health;
    }

    IEnumerator InvincibilityFlash()
    {
        isInvincible = true;
        float elapsed = 0f;

        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        isInvincible = false;
    }

    void ClampToCameraBounds()
    {
        if (mainCam == null) return;

        Vector3 minBounds = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxBounds = mainCam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minBounds.x + spriteHalfSize.x, maxBounds.x - spriteHalfSize.x);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minBounds.y + spriteHalfSize.y, maxBounds.y - spriteHalfSize.y);

        transform.position = clampedPos;
    }
}
