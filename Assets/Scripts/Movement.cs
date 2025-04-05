using UnityEngine;

public class Movement : MonoBehaviour

{
    public float velocidad = 5f; // Velocidad de movimiento de la nave

    void Update()
    {
        // Capturar entrada WASD
        float moverX = Input.GetAxisRaw("Horizontal");
        float moverY = Input.GetAxisRaw("Vertical");

        // Crear vector de dirección y normalizarlo
        Vector2 direccion = new Vector2(moverX, moverY).normalized;

        // Mover la nave (independiente del framerate)
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
    }
}

