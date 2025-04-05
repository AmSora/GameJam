using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public float velocidadProyectil = 10f;
    public float tiempoEntreDisparos = 1.5f;
    public float rangoDisparo = 5f;
    public int daño = 5;
    public Faccion faccion = Faccion.Enemigo;

    private Transform jugador;
    private float temporizadorDisparo;

    void Start()
    {
        GameObject objJugador = GameObject.FindWithTag("Player");
        if (objJugador != null)
        {
            jugador = objJugador.transform;
        }
        else
        {
            Debug.LogWarning("EnemyShooter: No se encontró al jugador con tag 'Player'");
        }

        temporizadorDisparo = 0f;
    }

    void Update()
    {
        if (jugador == null) return;

        temporizadorDisparo -= Time.deltaTime;

        float distancia = Vector2.Distance(transform.position, jugador.position);
        if (distancia <= rangoDisparo && temporizadorDisparo <= 0f)
        {
            Disparar();
            temporizadorDisparo = tiempoEntreDisparos;
        }
    }

    void Disparar()
    {
        GameObject bullet = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);

        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Asignar daño y facción al proyectil
        Bullet p = bullet.GetComponent<Bullet>();
        if (p != null)
        {
            p.daño = daño;
            p.faccionOrigen = faccion;
        }

        // Mover el proyectil
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDisparo);
    }
}
