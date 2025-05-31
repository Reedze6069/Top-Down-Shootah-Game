using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Character
{
    public bool shouldRotate = false;
    public float rotationSpeed = 180f; // Degrees per second

    public GameObject deathEffectPrefab; // Assign in Inspector

    private Transform player;

    private float nextDamageTime = 0f;
    public float damageInterval = 1f; // seconds between each damage tick

    void Start()
    {
        player = Object.FindFirstObjectByType<PlayerController>().transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (shouldRotate)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(other.GetComponent<Projectile>().damage);
        }
    }

    public override void HandleDeath()
    {
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1f); // cleanup after 1 second
        }
        else
        {
            Debug.LogWarning("Enemy deathEffectPrefab is not assigned.");
        }

        Destroy(gameObject);
    }
}