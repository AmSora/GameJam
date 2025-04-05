using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f); // Se autodestruye después de 2 segundos
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            Destroy(other.gameObject); // Elimina al enemigo
            Destroy(gameObject);       // Destruye la bala
        }
    }
}
