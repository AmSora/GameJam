using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int da�o = 3;
    public Faccion faccionOrigen;

    void OnTriggerEnter2D(Collider2D other)
    {
        TakeDamage objetivo = other.GetComponent<TakeDamage>();
        if (objetivo != null && objetivo.faccion != faccionOrigen)
        {
            objetivo.RecibirDa�o(da�o);
            Destroy(gameObject);
        }
    }
}
