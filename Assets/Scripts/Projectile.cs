using UnityEngine;

/// <summary>
/// Controls projectile behavior: movement, collision detection, and auto-destroy.
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    private bool isDestroyed = false;

    void Start()
    {
        // Ensure the Rigidbody2D is kinematic to avoid unwanted physics interactions
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Ensure collider is trigger for collision detection
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
        Destroy(gameObject, 2f);

    }

    void Update()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed) return;

        if (other.CompareTag("Enemy"))
        {
            // Optional: damage is already applied in Enemy.cs
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // Auto-destroy when the projectile exits the camera view
        Destroy(gameObject);
    }
}