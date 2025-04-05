using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform objetivo;          // El jugador
    public float velocidad = 3f;        // Velocidad de movimiento
    public float distanciaMinima = 0.5f; // Distancia a la que deja de acercarse

    void Start()
    {
        // Buscar autom�ticamente el jugador por tag
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            objetivo = jugador.transform;
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n objeto con el tag 'Player'");
        }
    }

    void Update()
    {
        if (objetivo == null) return;

        // Calcular direcci�n hacia el jugador
        Vector2 direccion = objetivo.position - transform.position;
        float distancia = direccion.magnitude;

        // Si est� lejos, moverse hacia el jugador
        if (distancia > distanciaMinima)
        {
            Vector2 direccionNormalizada = direccion.normalized;
            transform.position += (Vector3)(direccionNormalizada * velocidad * Time.deltaTime);
        }
    }
}