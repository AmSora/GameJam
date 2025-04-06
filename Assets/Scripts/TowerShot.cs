using UnityEngine;

public class TowerShot : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private float fireCooldown = 0f;

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (CanShootTarget(out Transform target))
        {
            if (fireCooldown <= 0f)
            {
                Shoot(target);
                fireCooldown = 1f / fireRate;
            }
        }

    }

    private bool CanShootTarget(out Transform target)
    {
        target = null;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                target = hit.transform;
                return true;
            }
        }
        return false;
    }

    private void Shoot(Transform target)
    {
        if (bulletPrefab != null && firePoint != null && target != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bulletComp = bullet.GetComponent<Bullet>();

            if (bulletComp != null)
            {
                bulletComp.colorID = GetComponent<TowerData>().towerTypeID;
                bulletComp.SetTarget(target); // Aquí usamos el seguimiento
            }
        }
    }

}
