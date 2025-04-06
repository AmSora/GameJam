using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public int colorID = -1;

    private Transform target;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform targetEnemy)
    {
        target = targetEnemy;

        if (target != null)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;
            rb.linearVelocity = direction * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                int totalDamage = damage;

                if (enemy.enemyTypeID == colorID)
                {
                    totalDamage += 1;
                }

                enemy.TakeDamage(totalDamage);
                Destroy(gameObject);
            }
        }
    }
}
