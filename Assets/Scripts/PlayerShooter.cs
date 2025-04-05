using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public float velocidadProyectil = 10f;
    public float tiempoRecarga = 1f;
    public int daño = 5;
    public Faccion faccion = Faccion.Jugador;

    public Transform objetivo; // <- Nuevo: objetivo hacia donde dispara

    private float temporizador;

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && temporizador <= 0f && objetivo != null)
        {
            Disparar();
            temporizador = tiempoRecarga;
        }
    }

    void Disparar()
    {
        // Calcular dirección hacia el objetivo
        Vector2 direccion = (objetivo.position - transform.position).normalized;

        // Instanciar proyectil
        GameObject bullet = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);

        // Configurar el proyectil
        Bullet p = bullet.GetComponent<Bullet>();
        if (p != null)
        {
            p.daño = daño;
            p.faccionOrigen = faccion;
        }

        // Aplicar velocidad
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direccion * velocidadProyectil;
        }
        else
        {
            bullet.transform.position += (Vector3)(direccion * velocidadProyectil * Time.deltaTime);
        }
    }
}
