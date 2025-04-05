using UnityEngine;
using UnityEngine.UI;
public class TowerButton : MonoBehaviour
{
    public GameObject torretaPrefab;

    private void Start()
    {
        Sprite sprite = torretaPrefab.GetComponent<SpriteRenderer>().sprite;
        GetComponent<Image>().sprite = sprite;
    }

    public void SeleccionarEstaTorreta()
    {
        UIManager.instance.SeleccionarTorreta(torretaPrefab);
    }
}
