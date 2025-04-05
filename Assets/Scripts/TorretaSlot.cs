using UnityEngine;

public class TorretaSlot : MonoBehaviour
{
    [Header("Configuración del Slot")]
    public bool sePuedeConstruir = true;
    public bool ocupado = false;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        OcultarResaltado();
    }

    public void MostrarResaltado()
    {
        if (!ocupado && sePuedeConstruir)
            sr.color = Color.green;
        else
            sr.color = Color.red;
    }

    public void OcultarResaltado()
    {
        sr.color = Color.white;
    }
}
