using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject torretaSeleccionada;
    public GameObject previewActual;

    private TorretaSlot[] slots;

    private void Awake()
    {
        instance = this;
    }

    public void SeleccionarTorreta(GameObject prefab)
    {
        torretaSeleccionada = prefab;

        // Crear preview fantasma
        if (previewActual != null)
            Destroy(previewActual);

        previewActual = Instantiate(prefab);
        Color colorFantasma = new Color(1f, 1f, 1f, 0.5f);
        previewActual.GetComponent<SpriteRenderer>().color = colorFantasma;

        // Desactivar lógica de torreta mientras es preview
        TowerShot shot = previewActual.GetComponent<TowerShot>();
        if (shot != null)
        {
            shot.enabled = false;
        }

        slots = FindObjectsOfType<TorretaSlot>();
        foreach (TorretaSlot slot in slots)
        {
            slot.MostrarResaltado();
        }
    }

    public void LimpiarSeleccion()
    {
        torretaSeleccionada = null;

        if (previewActual != null)
            Destroy(previewActual);

        if (slots == null) return;
        foreach (TorretaSlot slot in slots)
        {
            slot.OcultarResaltado();
        }
    }
}
