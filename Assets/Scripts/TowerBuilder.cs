using UnityEngine;

public class ConstructorTorretas : MonoBehaviour
{
    void Update()
    {
        if (UIManager.instance.torretaSeleccionada != null)
        {
            // Mover la preview
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UIManager.instance.previewActual.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

            if (Input.GetMouseButtonDown(0))
            {
                Collider2D hit = Physics2D.OverlapPoint(mousePos);
                if (hit != null)
                {
                    TorretaSlot slot = hit.GetComponent<TorretaSlot>();
                    if (slot != null && !slot.ocupado)
                    {
                        if (slot != null && !slot.ocupado && slot.sePuedeConstruir)
                        {
                            GameObject nuevaTorreta = Instantiate(UIManager.instance.torretaSeleccionada, slot.transform.position, Quaternion.identity);
                            slot.ocupado = true;
                            UIManager.instance.LimpiarSeleccion();
                        }
                    }
                }
            }
        }
    }
}
