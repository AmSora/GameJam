using UnityEngine;
using UnityEngine.UI;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private RawImage m_Image;
    [SerializeField] private float _x, _y;
    void Update()
    {
        m_Image.uvRect = new Rect(m_Image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, m_Image.uvRect.size);
    }
}
