using UnityEngine;

public class TowerShot : MonoBehaviour
{
    public float rango = 5f;
    public Transform parteRotatoria;
    public GameObject proyectilPrefab;
    public float velocidadProyectil = 10f;
    public float tiempoDisparo = 1f;

    private float temporizador;
    private Transform objetivo;

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (objetivo == null || Vector2.Distance(transform.position, objetivo.position) > rango)
        {
            BuscarNuevoObjetivo();
        }

        if (objetivo != null)
        {
            Apuntar();
            if (temporizador <= 0f)
            {
                Disparar();
                temporizador = tiempoDisparo;
            }
        }
    }

    void BuscarNuevoObjetivo()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        float distanciaMin = Mathf.Infinity;
        Transform mejorObjetivo = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector2.Distance(transform.position, enemigo.transform.position);
            if (distancia < rango && distancia < distanciaMin)
            {
                distanciaMin = distancia;
                mejorObjetivo = enemigo.transform;
            }
        }

        objetivo = mejorObjetivo;
    }

    void Apuntar()
    {
        Vector2 direccion = objetivo.position - parteRotatoria.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        parteRotatoria.rotation = Quaternion.Euler(0, 0, angulo);
    }

    void Disparar()
    {
        Vector2 direccion = (objetivo.position - transform.position).normalized;
        GameObject bala = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direccion * velocidadProyectil;
        }
        else
        {
            bala.transform.position += (Vector3)(direccion * velocidadProyectil * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
