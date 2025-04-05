using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public int vidaMaxima = 3;
    private int vidaActual;

    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log($"¡Has recibido {cantidad} de daño! Vida restante: {vidaActual}");

        if (vidaActual <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("¡GAME OVER!");
        // Aquí puedes pausar el juego, mostrar una UI, etc.
        Time.timeScale = 0;
    }
}
