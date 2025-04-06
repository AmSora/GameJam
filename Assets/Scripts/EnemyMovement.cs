using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    private Transform target;
    private int pathIndex = 0;

    IEnumerator Start()
    {
        // Esperar hasta que LevelManager esté inicializado y tenga al menos un punto en path
        while (LevelManager.main == null || LevelManager.main.path == null || LevelManager.main.path.Length == 0)
        {
            yield return null;
        }

        target = LevelManager.main.path[pathIndex];
    }

    void Update()
    {
        if (target == null) return;

        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex >= LevelManager.main.path.Length)
            {
                GameManager.main.TakeDamage(1);
                Destroy(gameObject);
                return;
            }

            target = LevelManager.main.path[pathIndex];
        }
    }
    public void ApplySlow(float factor, float duration)
    {
        if (!isSlowed)
            StartCoroutine(SlowCoroutine(factor, duration));
    }

    private bool isSlowed = false;

    private IEnumerator SlowCoroutine(float factor, float duration)
    {
        isSlowed = true;
        float originalSpeed = moveSpeed;
        moveSpeed *= factor;

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
        isSlowed = false;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

}
