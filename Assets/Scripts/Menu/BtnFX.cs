using UnityEngine;
using UnityEngine.EventSystems;

public class BtnFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] AudioSource audioSource;//Referencia al AudioSource
    [SerializeField] AudioClip sonidoEfecto;//Sonido cuando el mouse entra
    private Vector3 medidaOriginal;//Escala original

    void Start()
    {
        medidaOriginal = transform.localScale;//Guarda el tamaño inicial
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource && sonidoEfecto)
        {
            audioSource.PlayOneShot(sonidoEfecto);//Reproduce sonido
        }
        transform.localScale = medidaOriginal * 1.2f;//Aumenta el tamaño en 20%
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = medidaOriginal;//Vuelve al tamaño original
    }
}